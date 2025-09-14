// MusicPlayer.tsx
import React, { useEffect, useState } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  Alert,
  Platform,
  FlatList,
  ScrollView,
  SafeAreaView,
  useWindowDimensions,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";
import Slider from "@react-native-community/slider";
import * as DocumentPicker from "expo-document-picker";
import * as FileSystem from "expo-file-system";
import { Audio } from "expo-av";
import { useNavigation } from "@react-navigation/native";
import { StackNavigationProp } from "@react-navigation/stack";

type RootStackParamList = {
  MusicPlayer: undefined;
  Equalizer: undefined;
  Login: undefined;
};

type NavigationProp = StackNavigationProp<RootStackParamList>;

// Bundled songs
const SONGS = [
  { file: require("./assets/songs/song1.mp3"), title: "The Notorious B.I.G. - Hypnotize" },
  { file: require("./assets/songs/song2.mp3"), title: "Jordan Lee - Love Ride" },
  { file: require("./assets/songs/song3.mp3"), title: "Tyler, the Creator - Sugar On My Tongue" },
  { file: require("./assets/songs/song4.mp3"), title: "Ozzy Osbourne - I Don’t Wanna Stop" },
];

const MusicPlayer = () => {
  const navigation = useNavigation<NavigationProp>();
  const { width: SCREEN_WIDTH, height: SCREEN_HEIGHT } = useWindowDimensions();

  const [sound, setSound] = useState<Audio.Sound | null>(null);
  const [isPlaying, setIsPlaying] = useState(false);
  const [currentSong, setCurrentSong] = useState<{ file: string | number; title: string }>(SONGS[0]);
  const [recentFiles, setRecentFiles] = useState(SONGS);
  const [volume, setVolume] = useState(0.5);
  const [position, setPosition] = useState(0);
  const [duration, setDuration] = useState(1);

  const formatTime = (ms: number) => {
    const totalSeconds = Math.floor(ms / 1000);
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = totalSeconds % 60;
    return `${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;
  };

  useEffect(() => {
    const configureAudio = async () => {
      try {
        await Audio.setAudioModeAsync({
          playsInSilentModeIOS: true,
          allowsRecordingIOS: false,
          interruptionModeIOS: 1,
          interruptionModeAndroid: 1,
          shouldDuckAndroid: true,
          staysActiveInBackground: false,
          playThroughEarpieceAndroid: false,
        });
      } catch (err) {
        console.error("Audio configuration error:", err);
      }
    };
    configureAudio();

    return () => {
      sound?.unloadAsync();
    };
  }, []);

  const pickAudioFile = async () => {
    const result: any = await DocumentPicker.getDocumentAsync({ type: "audio/*" });
    if (result.type === "success") {
      let localUri = result.uri;
      if (Platform.OS === "android") {
        const dest = `${FileSystem.cacheDirectory}${result.name}`;
        await FileSystem.copyAsync({ from: result.uri, to: dest });
        localUri = dest;
      }
      const newSong = { file: localUri, title: result.name };
      setRecentFiles((prev) => [newSong, ...prev.filter((f) => f.file !== localUri)]);
      setCurrentSong(newSong);
      if (sound) {
        await sound.unloadAsync();
        setSound(null);
        setIsPlaying(false);
        setPosition(0);
        setDuration(1);
      }
    }
  };

  const playPauseSound = async (song?: string | number) => {
    let selectedSong = currentSong;
    if (song) {
      const found = recentFiles.find((s) => s.file === song);
      if (found) selectedSong = found;
    }

    if (sound && selectedSong.file !== currentSong.file) {
      await sound.unloadAsync();
      setSound(null);
      setIsPlaying(false);
      setPosition(0);
      setDuration(1);
    }

    setCurrentSong(selectedSong);

    try {
      if (!sound || selectedSong.file !== currentSong.file) {
        const source = typeof selectedSong.file === "string" ? { uri: selectedSong.file } : selectedSong.file;
        const { sound: newSound } = await Audio.Sound.createAsync(source, { shouldPlay: true, volume });
        setSound(newSound);
        setIsPlaying(true);
        newSound.setOnPlaybackStatusUpdate((status) => {
          if (!status.isLoaded) return;
          setIsPlaying(status.isPlaying);
          setPosition(status.positionMillis);
          setDuration(status.durationMillis ?? 1);
        });
        await newSound.playAsync();
      } else {
        if (isPlaying) await sound.pauseAsync();
        else await sound.playAsync();
      }
    } catch (err) {
      console.error("Audio error:", err);
      Alert.alert("Playback Error", "Unable to play this audio.");
    }
  };

  const stopSound = async () => {
    await sound?.stopAsync();
    await sound?.setPositionAsync(0);
    setIsPlaying(false);
    setPosition(0);
  };

  const changeVolume = async (val: number) => {
    setVolume(val);
    await sound?.setVolumeAsync(val);
  };

  const seekAudio = async (val: number) => {
    await sound?.setPositionAsync(val);
  };

  const handleLogout = () => {
    sound?.stopAsync();
    sound?.unloadAsync();
    navigation.replace("Login");
  };

  return (
    <SafeAreaView style={styles.safeArea}>
      <ScrollView
        contentContainerStyle={[styles.container, { paddingHorizontal: SCREEN_WIDTH * 0.05 }]}
        showsVerticalScrollIndicator={false}
      >
        {/* Top Buttons */}
        <View style={styles.topRow}>
          <TouchableOpacity style={styles.topButton} onPress={handleLogout}>
            <Ionicons name="log-out" size={28} color="#000" />
          </TouchableOpacity>
          <TouchableOpacity style={styles.topButton} onPress={() => navigation.navigate("Equalizer")}>
            <Text style={{ color: '#000', fontWeight: '800', fontSize: 24 }}>EQ</Text>
          </TouchableOpacity>
        </View>

        <Text style={styles.title}>Music Player</Text>
        <Text style={styles.currentSongText}>Now Playing: {currentSong.title}</Text>

        {/* Recent Files */}
        <View style={{ maxHeight: SCREEN_HEIGHT * 0.25, width: "100%", marginBottom: 20 }}>
          <FlatList
            data={recentFiles}
            keyExtractor={(item) => (typeof item.file === "string" ? item.file : item.title)}
            scrollEnabled={false}
            renderItem={({ item }) => (
              <TouchableOpacity style={styles.recentButton} onPress={() => playPauseSound(item.file)}>
                <Ionicons name="musical-notes-outline" size={20} color="#2cff02" />
                <Text style={styles.buttonText}>{item.title}</Text>
              </TouchableOpacity>
            )}
          />
        </View>

        <TouchableOpacity style={styles.selectButton} onPress={pickAudioFile}>
          <Ionicons name="folder" size={28} color="white" />
          <Text style={styles.buttonText}>Select Audio</Text>
        </TouchableOpacity>

        {/* Controls above Playback */}
        <View style={styles.controlsRow}>
          <TouchableOpacity style={styles.controlButton} onPress={() => playPauseSound()}>
            <Ionicons name={isPlaying ? "pause" : "play"} size={32} color="white" />
          </TouchableOpacity>
          <TouchableOpacity style={styles.controlButton} onPress={stopSound}>
            <Ionicons name="stop" size={32} color="red" />
          </TouchableOpacity>
        </View>

        {/* Playback Slider */}
        <Text style={styles.label}>Playback</Text>
        <Slider
          style={[styles.slider, { width: SCREEN_WIDTH * 0.9 }]}
          minimumValue={0}
          maximumValue={duration}
          value={position}
          onSlidingComplete={seekAudio}
          minimumTrackTintColor="#ffffffff"
          maximumTrackTintColor="#a2a2a2ff"
        />
        <View style={styles.timeRow}>
          <Text style={styles.timeText}>{formatTime(position)}</Text>
          <Text style={styles.timeText}>-{formatTime(duration - position)}</Text>
        </View>

        {/* Volume Slider */}
        <Text style={styles.label}>Volume</Text>
        <Text style={styles.timeText}>{Math.round(volume * 100)}%</Text>
        <Slider
          style={[styles.slider, { width: SCREEN_WIDTH * 0.9 }]}
          minimumValue={0}
          maximumValue={1}
          value={volume}
          onValueChange={changeVolume}
          minimumTrackTintColor="#2cff02"
          maximumTrackTintColor="#555"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

export default MusicPlayer;

const styles = StyleSheet.create({
  safeArea: { flex: 1, backgroundColor: "#121212" },
  container: { alignItems: "center", paddingVertical: 20 },
  topRow: { flexDirection: "row", justifyContent: "space-between", width: "100%", marginBottom: 20 },
  topButton: { width: 70, height: 50, backgroundColor: '#2cff02', borderRadius: 25, alignItems: 'center', justifyContent: 'center' },
  title: { fontSize: 36, color: "#2cff02", marginBottom: 10, fontWeight: "800", textAlign: "center" },
  currentSongText: { fontSize: 16, color: "#fff", marginBottom: 15, textAlign: "center" },
  selectButton: { flexDirection: "row", alignItems: "center", backgroundColor: "#282828", padding: 12, borderRadius: 25, marginBottom: 20 },
  recentButton: { flexDirection: "row", alignItems: "center", backgroundColor: "#282828", padding: 10, borderRadius: 20, marginBottom: 8 },
  buttonText: { color: "white", fontSize: 16, marginLeft: 8, fontWeight: "600" },
  controlsRow: { flexDirection: "row", marginTop: 10, gap: 16, marginBottom: 10 },
  controlButton: { flexDirection: "row", alignItems: "center", backgroundColor: "#282828", padding: 12, borderRadius: 20 },
  label: { color: "white", marginTop: 15, marginBottom: 5, fontWeight: "600" },
  slider: { height: 40 },
  timeRow: { flexDirection: "row", justifyContent: "space-between", width: "90%", marginTop: 5 },
  timeText: { color: "#2cff02", fontWeight: "600" },
});
