import React, { createContext, useContext, useState, useEffect } from 'react';
import { Audio } from 'expo-av';

interface AudioContextProps {
  sound: Audio.Sound | null;
  setSound: (s: Audio.Sound | null) => void;
  isPlaying: boolean;
  setIsPlaying: (p: boolean) => void;
  fileUri: string | null;
  setFileUri: (uri: string | null) => void;
  volume: number;
  setVolume: (v: number) => void;
  position: number;
  setPosition: (p: number) => void;
  duration: number;
  setDuration: (d: number) => void;
  resetAudio: () => Promise<void>; // <-- helper for cleanup
}

const AudioContext = createContext<AudioContextProps | undefined>(undefined);

export const AudioProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [sound, setSound] = useState<Audio.Sound | null>(null);
  const [isPlaying, setIsPlaying] = useState(false);
  const [fileUri, setFileUri] = useState<string | null>(null);
  const [volume, setVolume] = useState(1);
  const [position, setPosition] = useState(0);
  const [duration, setDuration] = useState(0);

  // Cleanup old sound if replaced
  const resetAudio = async () => {
    if (sound) {
      await sound.unloadAsync();
      setSound(null);
      setIsPlaying(false);
      setPosition(0);
      setDuration(0);
    }
  };

  return (
    <AudioContext.Provider
      value={{
        sound, setSound,
        isPlaying, setIsPlaying,
        fileUri, setFileUri,
        volume, setVolume,
        position, setPosition,
        duration, setDuration,
        resetAudio
      }}
    >
      {children}
    </AudioContext.Provider>
  );
};

export const useAudio = () => {
  const context = useContext(AudioContext);
  if (!context) throw new Error('useAudio must be used within AudioProvider');
  return context;
};