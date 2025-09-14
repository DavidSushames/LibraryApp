// App.tsx
import React from 'react';
import { 
  SafeAreaView, // Ensures content is placed within safe screen bounds
  StyleSheet,   // Used for styling components
  View,         // Basic container component
  Text,         // Text display component
  TextInput,    // Input field component
  TouchableOpacity, // Touchable component with visual feedback
  Platform     // Platform-specific constants
} from 'react-native';

/**
 * Main App component that renders the login screen
 */
export default function App() {
  // State management for form inputs
  const [email, setEmail] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');

  
   //Handles the login submission
   //Makes the API call
   
  const handleLogin = () => {
    console.log('Login attempted:', { email, password });
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
        {/* Email Field. Maybe make it a Username or Email field. */}
        <TextInput
          style={styles.input}
          placeholder="Email Address"
          autoCapitalize="none"
          keyboardType="email-address"
          textContentType="emailAddress"
          value={email}
          onChangeText={(text) => setEmail(text)}
        />

        {/* Password Input Field */}
        <TextInput
          style={[styles.input, styles.passwordInput]}
          placeholder="Password"
          secureTextEntry={true}
          textContentType="password"
          value={password}
          onChangeText={(text) => setPassword(text)}
        />

        {/* Login Button */}
        <TouchableOpacity 
          style={styles.loginButton}
          onPress={handleLogin}
        >
          <Text style={styles.loginButtonText}>Log In</Text>
        </TouchableOpacity>

        {/* Forgot Password Link. On press, send the password to the email? Or maybe just remove this option :/  */}
        <TouchableOpacity onPress={() => console.log('Forgot password pressed')}>
          <Text style={styles.forgotPassword}>Forgot Password?</Text>
        </TouchableOpacity>
      </View>

      {/* Sign-up Section */}
      <View style={styles.signupSection}>
        <Text style={styles.signupText}>
          Don't have an account?
          <TouchableOpacity onPress={() => console.log('Sign up pressed')}>
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
    backgroundColor: '#ffffff',
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
    marginBottom: 50,
    marginTop: Platform.select({ ios: 150, android: 150 }), // Adjust for iOS notch
    alignItems: 'center',
    
  },

  title: {
    fontSize: 28,
    fontWeight: '600',
    color: '#333333',
    textAlign: 'center',
  },

  form: {
    width: '100%',
    paddingHorizontal: 25,
  },

  input: {
    height: 50,
    borderWidth: 1,
    borderColor: '#e5e5e5',
    borderRadius: 8,
    paddingHorizontal: 16,
    marginVertical: 8,
    fontSize: 16,
    backgroundColor: '#ffffff',
  },

  passwordInput: {
    marginBottom: 16,
  },

  loginButton: {
    backgroundColor: '#1a80e7ff', // Blue color matching typical login buttons
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
    marginTop: 8,
    marginBottom: 16,
  },

  loginButtonText: {
    color: '#ffffff',
    fontSize: 18,
    fontWeight: '500',
  },

  forgotPassword: {
    textAlign: 'center',
    fontSize: 14,
    color: '#0063c7ff',
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
    justifyContent: 'center',
    textAlignVertical: 'center',
  },

  signupLink: {
    color: '#0066cc', //Otherwise the Sign Up link is higher up than the other text.
    fontWeight: '500',
    fontSize: -4.5,
  },
});