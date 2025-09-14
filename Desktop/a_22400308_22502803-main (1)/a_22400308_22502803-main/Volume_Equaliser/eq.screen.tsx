// EqualizerScreen.tsx
import React, { useState, useLayoutEffect, useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  PanResponder,
  Platform,
  useWindowDimensions,
  Modal,
  FlatList,
} from 'react-native';
import { StatusBar as ExpoStatusBar } from 'expo-status-bar';
import { StatusBar as RNStatusBar } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import Slider from '@react-native-community/slider';
import { useNavigation } from '@react-navigation/native';
import { saveData, getData } from './eqapp/utils/storage';
import { 
  setEQBand, 
  setVolume as setPlayerVolume, 
  getVolume, 
  addVolumeListener, 
  removeVolumeListener 
} from './AudioService';

const TRACK_WIDTH = 6;
const THUMB_SIZE = 30;

/* ----------------------------- Vertical Slider ---------------------------- */
const VerticalSlider: React.FC<{
  value: number;
  onValueChange: (value: number) => void;
  setIsDraggingParent?: (dragging: boolean) => void;
  minimumValue?: number;
  maximumValue?: number;
  height?: number;
  width?: number;
  trackColor?: string;
  thumbColor?: string;
}> = ({
  value,
  onValueChange,
  setIsDraggingParent,
  minimumValue = 0,
  maximumValue = 100,
  height = 200,
  width = 40,
  trackColor = '#2cff02',
  thumbColor = '#2cff02',
}) => {
  const [isDragging, setIsDragging] = useState(false);

  const valueToY = (val: number) =>
    height - ((val - minimumValue) / (maximumValue - minimumValue)) * height;

  const yToValue = (y: number) => {
    const clamped = Math.max(0, Math.min(height, y));
    const pct = (height - clamped) / height;
    return minimumValue + pct * (maximumValue - minimumValue);
  };

  const panResponder = PanResponder.create({
    onStartShouldSetPanResponder: () => true,
    onMoveShouldSetPanResponder: () => true,
    onPanResponderTerminationRequest: () => false,
    onPanResponderGrant: (evt) => {
      setIsDragging(true);
      setIsDraggingParent?.(true);
      onValueChange(Math.round(yToValue(evt.nativeEvent.locationY)));
    },
    onPanResponderMove: (evt) => {
      onValueChange(Math.round(yToValue(evt.nativeEvent.locationY)));
    },
    onPanResponderRelease: () => {
      setIsDragging(false);
      setIsDraggingParent?.(false);
    },
  });

  const currentY = valueToY(value);
  const activeHeight = height - currentY;

  return (
    <View style={[styles.verticalSliderContainer, { height, width }]}>
      <View
        style={[
          styles.track,
          { height, width: TRACK_WIDTH, left: (width - TRACK_WIDTH) / 2 },
        ]}
      />
      <View
        style={[
          styles.activeTrack,
          {
            height: activeHeight,
            width: TRACK_WIDTH,
            left: (width - TRACK_WIDTH) / 2,
            backgroundColor: trackColor,
          },
        ]}
      />
      <View
        style={[
          styles.thumb,
          {
            top: currentY - THUMB_SIZE / 2,
            left: (width - THUMB_SIZE) / 2,
            width: THUMB_SIZE,
            height: THUMB_SIZE,
            borderRadius: THUMB_SIZE / 2,
            backgroundColor: thumbColor,
            opacity: isDragging ? 0.9 : 1,
            transform: [{ scale: isDragging ? 1.06 : 1 }],
          },
        ]}
      />
      <View {...panResponder.panHandlers} style={StyleSheet.absoluteFill} pointerEvents="box-only" />
    </View>
  );
};

/* --------------------------------- Screen -------------------------------- */
export default function EqualizerScreen() {
  const navigation = useNavigation();
  useLayoutEffect(() => {
    navigation.setOptions?.({
      headerStyle: { backgroundColor: '#000' },
      headerTintColor: '#2cff02',
      headerTitleStyle: { color: '#fff' },
      headerShadowVisible: false,
    } as any);
  }, [navigation]);

  const [bands, setBands] = useState<number[]>([50, 50, 50, 50, 50, 50]);
  const [volume, setVolume] = useState(50);
  const [isDraggingSlider, setIsDraggingSlider] = useState(false);
  const [presetModalVisible, setPresetModalVisible] = useState(false);
  const [activePreset, setActivePreset] = useState<string>('Flat');

  const freqLabels = ['60Hz', '150Hz', '400Hz', '1KHz', '2.4kHz', '15kHz'];
  const { width: SCREEN_WIDTH, height: SCREEN_HEIGHT } = useWindowDimensions();

  const CONTAINER_PADDING = SCREEN_WIDTH * 0.04;
  const SLIDER_SPACING = SCREEN_WIDTH * 0.02;
  const SLIDER_HEIGHT = Math.min(SCREEN_HEIGHT * 0.35, 280);
  const SLIDER_WIDTH = Math.max(
    36,
    Math.min(
      50,
      (SCREEN_WIDTH - CONTAINER_PADDING * 2 - SLIDER_SPACING * 5) / 6
    )
  );

  const EQ_PRESETS: { [key: string]: number[] } = {
    Rock: [60, 55, 50, 55, 60, 65],
    Classical: [50, 55, 60, 55, 50, 50],
    HipHop: [70, 65, 55, 50, 50, 45],
    Vocals: [50, 60, 65, 60, 55, 50],
    BassBoosted: [75, 70, 60, 50, 45, 40],
    RnB: [65, 60, 55, 55, 60, 65],
    Flat: [50, 50, 50, 50, 50, 50],
    Custom: [0, 0, 0, 0, 0, 0],
  };

  // Sync volume on mount and listen for changes
  useEffect(() => {
    // Set initial volume (convert 0-1 to 0-100)
    const currentVolume = getVolume();
    setVolume(Math.round(currentVolume * 100));

    // Listen for volume changes from MusicPlayer screen
    const handleVolumeChange = (newVolume: number) => {
      setVolume(Math.round(newVolume * 100));
    };

    addVolumeListener(handleVolumeChange);

    // Cleanup listener on unmount
    return () => {
      removeVolumeListener(handleVolumeChange);
    };
  }, []);

  useEffect(() => {
    (async () => {
      const savedBands = await getData('customEQ');
      if (savedBands && Array.isArray(savedBands) && savedBands.length === 6) {
        setBands(savedBands);
        setActivePreset('Custom');
        savedBands.forEach((val, idx) => setEQBand(idx, val));
      }
    })();
  }, []);

  const handleSaveConfig = async () => {
    EQ_PRESETS.Custom = [...bands];
    setActivePreset('Custom');
    await saveData('customEQ', bands);
    alert('Custom preset saved!');
  };

  const handleLoadConfig = () => setPresetModalVisible(true);

  const loadPreset = async (name: string) => {
    let presetBands: number[] = [];
    if (name === 'Custom') {
      const savedBands = await getData('customEQ');
      if (savedBands && Array.isArray(savedBands) && savedBands.length === 6) {
        presetBands = savedBands;
      } else return;
    } else if (EQ_PRESETS[name]) {
      presetBands = EQ_PRESETS[name];
    } else return;

    setBands(presetBands);
    setActivePreset(name);
    presetBands.forEach((val, idx) => setEQBand(idx, val));
    setPresetModalVisible(false);
  };

  const updateBand = (index: number, value: number) => {
    const next = [...bands];
    next[index] = value;
    setBands(next);
    setEQBand(index, value);
    if (activePreset !== 'Custom') setActivePreset('Custom');
  };

  const handleVolumeChange = async (val: number) => {
    setVolume(val);
    const volumeNormalized = val / 100; // Convert 0-100 to 0-1
    await setPlayerVolume(volumeNormalized);
  };

  return (
    <SafeAreaView style={styles.safeArea} edges={['bottom']}>
      <ExpoStatusBar style="light" translucent />
      {Platform.OS === 'android' && (
        <View style={{ height: RNStatusBar.currentHeight, backgroundColor: '#000' }} />
      )}

      <ScrollView
        contentContainerStyle={[styles.container, { padding: CONTAINER_PADDING }]}
        showsVerticalScrollIndicator={false}
        scrollEnabled={!isDraggingSlider}
      >
        {/* Top buttons */}
        <View style={styles.topButtons}>
          <TouchableOpacity
            style={styles.backButton}
            onPress={() => {
              if (navigation.canGoBack && navigation.canGoBack()) navigation.goBack();
              else (navigation as any).navigate('MusicPlayer');
            }}
          >
            <Text style={styles.buttonText}>Back to Player</Text>
          </TouchableOpacity>
          <View style={styles.rightButtons}>
            <TouchableOpacity style={styles.rightButtonsButton} onPress={handleSaveConfig}>
              <Text style={styles.buttonText}>Save Config</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.rightButtonsButton} onPress={handleLoadConfig}>
              <Text style={styles.buttonText}>Load Profile</Text>
            </TouchableOpacity>
          </View>
        </View>

        {/* Equalizer */}
        <View style={styles.eqContainer}>
          <Text style={styles.eqTitle}>Equalizer</Text>
          <Text style={styles.activePresetText}>Current Preset: {activePreset}</Text>
          <View style={styles.scaleRow}>
            <Text style={styles.scaleText}>+12dB</Text>
            <Text style={styles.scaleText}>0dB</Text>
            <Text style={styles.scaleText}>-12dB</Text>
          </View>
          <View style={styles.slidersRow}>
            {bands.map((band, idx) => (
              <View key={idx} style={styles.sliderColumn}>
                <Text style={styles.valueText}>
                  {band > 50 ? '+' : ''}{Math.round((band - 50) * 0.24)}
                </Text>
                <VerticalSlider
                  value={band}
                  onValueChange={(val) => updateBand(idx, val)}
                  setIsDraggingParent={setIsDraggingSlider}
                  height={SLIDER_HEIGHT}
                  width={SLIDER_WIDTH}
                />
                <Text style={styles.freqLabel}>{freqLabels[idx]}</Text>
              </View>
            ))}
          </View>
        </View>

        {/* Volume */}
        <View style={styles.volumeContainer}>
          <Text style={styles.volumeTitle}>Volume Control</Text>
          <Slider
            style={styles.volumeSlider}
            minimumValue={0}
            maximumValue={100}
            value={volume}
            onValueChange={handleVolumeChange}
            minimumTrackTintColor="#2cff02"
            maximumTrackTintColor="#555"
            thumbTintColor="#2cff02"
          />
          <Text style={styles.volumeValue}>{Math.round(volume)}%</Text>
        </View>

        {/* Preset Modal */}
        <Modal visible={presetModalVisible} transparent animationType="slide">
          <View style={styles.modalOuter}>
            <View style={styles.modalInner}>
              <Text style={styles.modalTitle}>Select EQ Preset</Text>
              <FlatList
                data={['Rock','Classical','HipHop','Vocals','BassBoosted','RnB','Flat','Custom']}
                keyExtractor={(item) => item}
                renderItem={({ item }) => (
                  <TouchableOpacity
                    onPress={() => loadPreset(item)}
                    style={[styles.modalOption, item === activePreset ? { backgroundColor: '#1DB954' } : null]}
                  >
                    <Text style={styles.modalOptionText}>{item}</Text>
                  </TouchableOpacity>
                )}
              />
              <TouchableOpacity onPress={() => setPresetModalVisible(false)} style={styles.modalCancel}>
                <Text style={styles.modalCancelText}>Cancel</Text>
              </TouchableOpacity>
            </View>
          </View>
        </Modal>
      </ScrollView>
    </SafeAreaView>
  );
}

/* ------------------------------- Styles --------------------------------- */
const styles = StyleSheet.create({
  safeArea: { flex: 1, backgroundColor: '#000' },
  container: { flexGrow: 1, backgroundColor: '#000', alignItems: 'center' },
  topButtons: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 20, marginTop: 50, width: '100%' },
  backButton: { paddingVertical: 12, paddingHorizontal: 12, borderRadius: 12, backgroundColor: '#2cff02', borderWidth: 1, borderColor: '#000', alignItems: 'center' },
  rightButtons: { flexDirection: 'row', alignItems: 'center' },
  rightButtonsButton: { paddingVertical: 12, paddingHorizontal: 16, borderRadius: 12, backgroundColor: '#2cff02', borderWidth: 1, borderColor: '#000', alignItems: 'center', marginLeft: 8 },
  buttonText: { fontSize: 16, fontWeight: '800', color: '#000' },
  eqContainer: { backgroundColor: '#242424', borderRadius: 16, padding: 20, marginBottom: 20, width: '100%', ...Platform.select({ ios:{ shadowColor:'#000', shadowOffset:{width:0,height:2}, shadowOpacity:0.1, shadowRadius:4 }, android:{ elevation:3 } }) },
  eqTitle: { fontSize: 18, fontWeight: 'bold', textAlign: 'center', marginBottom: 16, color: '#2cff02' },
  activePresetText: { fontSize: 14, fontWeight: '600', color: '#2cff02', textAlign: 'center', marginBottom: 10 },
  scaleRow: { flexDirection: 'row', justifyContent: 'space-between', width: '100%', marginBottom: 10, paddingHorizontal: 10 },
  scaleText: { fontSize: 12, color: '#fff', fontWeight: '600' },
  slidersRow: { flexDirection: 'row', justifyContent: 'space-between', width: '100%' },
  sliderColumn: { alignItems: 'center', justifyContent: 'flex-end' },
  valueText: { fontSize: 12, fontWeight: '600', color: '#2cff02', marginBottom: 8, minHeight: 20, textAlign: 'center' },
  freqLabel: { fontSize: 12, fontWeight: '500', color: '#fff', marginTop: 10, textAlign: 'center' },
  volumeContainer: { backgroundColor: '#242424', borderRadius: 16, padding: 20, width: '100%', ...Platform.select({ ios:{ shadowColor:'#000', shadowOffset:{width:0,height:4}, shadowOpacity:0.1, shadowRadius:4 }, android:{ elevation:3 } }) },
  volumeTitle: { fontSize: 18, fontWeight: 'bold', textAlign: 'center', marginBottom: 16, color: '#fff' },
  volumeSlider: { width: '100%', height: 40, ...Platform.select({ android:{ height: 8 } }) },
  volumeValue: { textAlign: 'center', marginTop: 10, fontSize: 16, fontWeight: '800', color: '#2cff02' },
  verticalSliderContainer: { justifyContent: 'flex-end', alignItems: 'center', position: 'relative' },
  track: { position: 'absolute', bottom: 0, backgroundColor: '#fff', borderRadius: 3 },
  activeTrack: { position: 'absolute', bottom: 0, borderRadius: 3 },
  thumb: { position: 'absolute', borderWidth: 3, borderColor: '#fff', ...Platform.select({ ios:{ shadowColor:'#000', shadowOffset:{width:0,height:3}, shadowOpacity:0.3, shadowRadius:5 }, android:{ elevation:6 } }) },
  modalOuter: { flex: 1, backgroundColor: 'rgba(0,0,0,0.6)', justifyContent: 'center', alignItems: 'center' },
  modalInner: { backgroundColor: '#242424', borderRadius: 16, width: '80%', padding: 20 },
  modalTitle: { color: '#2cff02', fontSize: 18, fontWeight: 'bold', marginBottom: 12, textAlign: 'center' },
  modalOption: { padding: 12, marginVertical: 4, backgroundColor: '#2cff02', borderRadius: 12, alignItems: 'center' },
  modalOptionText: { color: '#000', fontWeight: 'bold' },
  modalCancel: { marginTop: 10, padding: 10, alignItems: 'center' },
  modalCancelText: { color: '#fff', fontWeight: 'bold' },
});