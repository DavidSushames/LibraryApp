import React from "react"; 
import {createStackNavigator} from '@react-navigation/stack';
import { NavigationContainer } from "@react-navigation/native";
import LoginScreen from "./login.screen"; // Import the login screen component
import SignUpPage from "./signup.screen";
import EqualizerScreen from "./eq.screen"; // Import the equalizer screen component

const { Navigator, Screen } = createStackNavigator();

const AppNavigator = () => (
    <NavigationContainer>
        <Navigator initialRouteName="Login">
           <Screen name = "Login" component={LoginScreen}></Screen>
           <Screen name = "SignUp" component={SignUpPage}></Screen>
           <Screen name = "Equalizer" component={EqualizerScreen}></Screen>
        </Navigator>
    </NavigationContainer>
)

export default AppNavigator;