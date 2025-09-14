// AppNavigator.tsx
import React from "react";
import { createStackNavigator } from "@react-navigation/stack";

import LoginScreen from "./login.screen";
import MusicPlayer from "./musicplayer";
import SignUpPage from "./signup.screen";
import EqualizerScreen from "./eq.screen";

const Stack = createStackNavigator();

export default function AppNavigator() {
  return (
    <Stack.Navigator initialRouteName="MusicPlayer">
      <Stack.Screen
        name="MusicPlayer"
        component={MusicPlayer}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Login"
        component={LoginScreen}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="SignUp"
        component={SignUpPage}
        options={{ headerShown: false }}
      />
      <Stack.Screen
        name="Equalizer"
        component={EqualizerScreen}
        options={{ headerShown: false }}
      />
    </Stack.Navigator>
  );
}


