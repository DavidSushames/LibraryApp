// AudioService.tsx
import { Audio } from "expo-av";
import { Platform } from "react-native";

let audioSound: Audio.Sound | null = null;

// Global volume state (0 to 1)
let globalVolume: number = 0.5;
let volumeListeners: ((volume: number) => void)[] = [];

// Store 6-band EQ values (0-100)
let eqBands: number[] = [50, 50, 50, 50, 50, 50];

const EQ_PRESETS: Record<string, number[]> = {
  Rock: [60, 55, 50, 55, 60, 65],
  Classical: [50, 55, 60, 55, 50, 50],
  HipHop: [70, 65, 55, 50, 50, 45],
  Vocals: [50, 60, 65, 60, 55, 50],
  BassBoosted: [75, 70, 60, 50, 45, 40],
  RnB: [65, 60, 55, 55, 60, 65],
  Flat: [50, 50, 50, 50, 50, 50],
  Custom: [...eqBands],
};

/**
 * Add volume change listener
 */
export const addVolumeListener = (listener: (volume: number) => void) => {
  volumeListeners.push(listener);
};

/**
 * Remove volume change listener
 */
export const removeVolumeListener = (listener: (volume: number) => void) => {
  volumeListeners = volumeListeners.filter(l => l !== listener);
};

/**
 * Notify all listeners of volume change
 */
const notifyVolumeChange = (volume: number) => {
  volumeListeners.forEach(listener => listener(volume));
};

/**
 * Get current global volume
 */
export const getVolume = (): number => {
  return globalVolume;
};

/**
 * Set volume and update audio if playing
 */
export const setVolume = async (val: number) => {
  globalVolume = Math.max(0, Math.min(1, val)); // Clamp between 0-1
  
  // Update audio playback volume
  if (audioSound) {
    await audioSound.setVolumeAsync(globalVolume);
  }
  
  // Notify all listeners
  notifyVolumeChange(globalVolume);
};

/**
 * Setup a new audio track with current volume
 */
export const setupPlayer = async (uri: string | number) => {
  try {
    if (audioSound) {
      await audioSound.unloadAsync();
      audioSound = null;
    }

    const source = typeof uri === "string" ? { uri } : uri;

    const { sound } = await Audio.Sound.createAsync(source, {
      shouldPlay: false,
      volume: globalVolume,
    });

    audioSound = sound;
    return sound;
  } catch (error) {
    console.error('Audio setup error:', error);
    throw error;
  }
};

/** Playback controls */
export const play = async () => {
  if (audioSound) {
    return audioSound.playAsync();
  }
};

export const pause = async () => {
  if (audioSound) {
    return audioSound.pauseAsync();
  }
};

export const stop = async () => {
  if (!audioSound) return;
  await audioSound.stopAsync();
  await audioSound.setPositionAsync(0);
};

export const seek = async (ms: number) => audioSound && audioSound.setPositionAsync(ms);

/** EQ handling */
export const setEQBand = (index: number, value: number) => {
  eqBands[index] = value;
  console.log(`EQ Band ${index} set to ${value}`);
  // Simulate EQ effect - in real app, this would use audio processing
};

export const getEQBands = () => [...eqBands];

export const setEQMode = (mode: string) => {
  if (EQ_PRESETS[mode]) {
    eqBands = [...EQ_PRESETS[mode]];
    console.log(`EQ mode set: ${mode}`, eqBands);
  } else {
    console.warn(`Unknown EQ mode: ${mode}`);
  }
};

/** Get current playback status */
export const getPlaybackStatus = async () => {
  if (audioSound) {
    return await audioSound.getStatusAsync();
  }
  return null;
};

/** Cleanup */
export const cleanupAudio = async () => {
  if (audioSound) {
    await audioSound.unloadAsync();
    audioSound = null;
  }
};

/** Playlist placeholder */
export const addTrack = (uri: string | number) => {
  console.log("Added track:", uri);
};