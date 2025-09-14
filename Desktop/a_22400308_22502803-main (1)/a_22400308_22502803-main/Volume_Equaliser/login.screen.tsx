// App.tsx
import React, {useLayoutEffect, useState, useEffect} from 'react';
import { 
  SafeAreaView, // Ensures content is placed within safe screen bounds
  StyleSheet,   // Used for styling components
  View,         // Basic container component
  Text,         // Text display component
  TextInput,    // Input field component
  TouchableOpacity, // Touchable component with visual feedback
  Platform,     // Platform-specific constants
  Alert,
  
} from 'react-native';
import { loginUser, getCurrentUser, logoutUser } from './eqapp/utils/storage';
import AsyncStorage from '@react-native-async-storage/async-storage';

/**
 * Main App component that renders the login screen
 */
interface LoginScreenProps {
  navigation: any; 
}
export default function LoginScreen({ navigation }: LoginScreenProps) {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [currentUser, setCurrentUser] = useState<any>(null);
  // State management for form inputs
  useLayoutEffect(() => {
    navigation.setOptions?.({
      headerStyle: { backgroundColor: '#000' },
      headerTintColor: '#2cff02',
      headerTitleStyle: { color: '#fff' },
      headerShadowVisible: false,
    } as any);
  }, [navigation]);
  
  //Auto check if use is already logged in 
  useEffect (() => {
    const fetchUser = async () => {
      const user = await getCurrentUser();
      if (user) {
        setCurrentUser(user);
        navigation.replace("Equalizer"); //this will skip if the uers is already logged in 
      }
    }
    fetchUser();
  }, []);

  //Handles the login submission
  //Makes the API call
   
  const handleLogin = async () => {
    const result = await loginUser(email, password);
    if (result.success) {
      const user = await getCurrentUser();
      setCurrentUser(user);
      Alert.alert("Success", "Logged in!");
      navigation.replace("MusicPlayer");
    } else {
      Alert.alert("Error", result.message);
    }
    // TODO: Add login logic here
  };

  return (
    <SafeAreaView style={styles.container}>
      {/* Header Section */}
      <View style={styles.header}>
        <Text style={styles.title}>Volume Equaliser</Text>
      </View>

      {/* Form Section */}
      <View style={styles.form}>
        <TextInput
          style={styles.input}
          placeholder="Email Address"
          autoCapitalize="none"
          keyboardType="email-address"
          textContentType="emailAddress"
          value={email}
          onChangeText={setEmail}
        />

        {/* Password Input Field */}
        <TextInput
          style={styles.input}
          placeholder="Password"
          secureTextEntry
          textContentType="password"
          value={password}
          onChangeText={setPassword}
        />

        {/* Login Button */}
        <TouchableOpacity 
          style={styles.loginButton} onPress={handleLogin}>
          <Text style={styles.loginButtonText}>Log In</Text>
        </TouchableOpacity>

        {/* Forgot Password Link. On press, send the password to the email? Or maybe just remove this option :/  */}
        <TouchableOpacity onPress={() => console.log('Forgot password')}>
          <Text style={styles.forgotPassword}>Forgot Password?</Text>
        </TouchableOpacity>
      </View>

      {/* Sign-up Section */}
      <View style={styles.signupSection}>
        <Text style={styles.signupText}>
          Don't have an account?
          <TouchableOpacity onPress={() => navigation.navigate('SignUp')}>
            <Text style={styles.signupLink}> Sign Up</Text>
          </TouchableOpacity>
        </Text>
      </View>
    </SafeAreaView>
  );
}

/**
 * Styles for the login screen
 * Using Platform.select for platform-specific styling where needed
 */
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#000000ff',
    paddingHorizontal: 24,
    paddingTop: 20,
    
  },
  centerWrapper: {
    flex: 1,
    justifyContent: 'center',
    paddingVertical: 60,
    paddingHorizontal: 20,
  },
  
  header: {
    marginBottom: 30,
    marginTop: Platform.select({ ios: 110, android: 50 }), // Adjust for iOS notch
    alignItems: 'center',
    
  },

  title: {
    fontSize: Platform.select({ ios: 40, android: 28 }),
    fontWeight: '800',
    color: '#2cff02',
    textAlign: 'center',
  },

  form: {
    width: '100%',
    paddingHorizontal: 25,
  },

  input: {
    height: 50,
    borderWidth: 1,
    borderColor: '#000000ff',
    borderRadius: 8,
    paddingHorizontal: 16,
    marginVertical: 8,
    fontSize: 16,
    backgroundColor: '#dededeff',
  },

  passwordInput: {
    marginBottom: 16,
  },

  loginButton: {
    backgroundColor: '#2cff02', // Blue color matching typical login buttons
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
    marginTop: 8,
    marginBottom: 16,
  },

  loginButtonText: {
    color: '#000000ff',
    fontSize: 20,
    fontWeight: '600',
  },

  forgotPassword: {
    textAlign: 'center',
    fontSize: 14,
    color: '#ff0800ff',
    marginBottom: 8,
  },

  signupSection: {
    alignItems: 'center',
    paddingHorizontal: 2,
    
  },

  signupText: {
    color: '#ffffffd4',
    fontSize: 14,
    flexDirection: 'row',
    alignItems: 'center',
  },

  signupLink: {
    color: '#1683efff', //Otherwise the Sign Up link is higher up than the other text.
    fontWeight: '500',
    fontSize: 14,
    marginBottom: -3.5,
  },
});