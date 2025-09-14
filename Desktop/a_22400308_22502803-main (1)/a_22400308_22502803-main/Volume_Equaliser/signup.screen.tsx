// App.tsx
import React, {useLayoutEffect, useState} from 'react';
import { 
  SafeAreaView, // Ensures content is placed within safe screen bounds
  StyleSheet,   // Used for styling components
  View,         // Basic container component
  Text,         // Text display component
  TextInput,    // Input field component
  TouchableOpacity, // Touchable component with visual feedback
  Platform,     // Platform-specific constants
  Alert
  

} from 'react-native';
import { registerUser } from './eqapp/utils/storage';

interface SignUpPageProps {
  navigation: any;
}


/**
 * Main App component that renders the login screen
 */
export default function SignUp({ navigation }: SignUpPageProps) {
  // State management for form inputs
  useLayoutEffect (() => {
    navigation.setOptions?.({
      headerStyle: { backgroundColor: '#000' },
      headerTintColor: '#2cff02',
      headerTitleStyle: { color: '#fff' },
      headerShadowVisible: false,
    } as any);
  }, [navigation]);
  const [firstname, setfirstName] = useState<string>('');
  const [lastname, setlastName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [confirmPassword, setConfirmPassword] = useState<string>('');

  //Handles the login submission
  //Makes the API call

  const handleRegister = async () => {
    if (!firstname || !lastname || !email || !password || !confirmPassword) {
      Alert.alert("Error", "Please fill in all the fields");
      return;
    }
    if (password !== confirmPassword) {
      Alert.alert("Error", "Password do not match");
      return;
    }

    const result = await registerUser({email, password, firstname, lastname});
    if (result.success) {
      Alert.alert("Success", "Account created successfully!");
      navigation.replace("Login");
    }else{
      Alert.alert("Error", result.message);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      {/* Header Section */}
      <View style={styles.header}>
        <Text style={styles.title}>Create Account</Text>
        
      </View>

      {/* Form Section */}
      <View style={styles.form}>
        {/* Email Field. Maybe make it a Username or Email field. */}
        <TextInput
          style={styles.input}
          placeholder="First Name"
          value={firstname}
          onChangeText={setfirstName}
        />

        {/* Last Name Field */}
        <TextInput
          style={styles.input}
          placeholder="Last Name"
          value={lastname}
          onChangeText={setlastName}
        />

        {/* Email Field */}
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
          secureTextEntry={true}
          textContentType="password"
          value={password}
          onChangeText={(text) => setPassword(text)}
        />

        <TextInput
          style={styles.input}
          placeholder="Confirm Password"
          secureTextEntry={true}
          textContentType="password"
          value={confirmPassword}
          onChangeText={(text) => setConfirmPassword(text)}
        />

        {/* Register Button */}
        <TouchableOpacity 
          style={styles.loginButton}
          onPress={handleRegister}
        >
          <Text style={styles.loginButtonText}>SignUp</Text>
        </TouchableOpacity>

        {/*Back to Login*/}
        <View style={styles.container}>
          <Text style={styles.container}>Already have an account?</Text>
          <TouchableOpacity onPress={() => navigation.navigate('LoginPage')}>
            <Text style={styles.signupLink}> Log In</Text>
          </TouchableOpacity>
        </View>
      </View>

      {/* Sign-up Section */}
      
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
    marginBottom: Platform.select({ ios: 30, android: 20 }), // Adjust for iOS notch
    marginTop: Platform.select({ ios: 20, android: 120 }), // Adjust for iOS notch
    alignItems: 'center',
    
  },

  title: {
    fontSize: Platform.select({ ios: 40, android: 36 }),
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
    color: '#666666',
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
