import AsyncStorage from '@react-native-async-storage/async-storage';

interface User {
    email: string;
    password: string;
    firstname: string;
    lastname: string;
}
export const saveData = async (key: string, value: any) => {
    try {
        const jsonValue = JSON.stringify(value);
        await AsyncStorage.setItem(key, jsonValue);
    } catch (e) {
        console.error("Error saving data:", e);
    }
};

export const getData = async (key: string) => {
    try {
        const jsonValue = await AsyncStorage.getItem(key);
        return jsonValue != null ? JSON.parse(jsonValue) : null;
    } catch (e) {
        console.error("Error reading data:", e);
        return null;
    }
};

export const removeData = async (key: string) => {
    try {
        await AsyncStorage.removeItem(key);
    } catch (e) {
        console.error("Error removing data:", e);
    }
};

export const registerUser = async (user: User)=> {
    try {
        const users: User[] = (await getData("users")) || [];
        if (users.some(u => u.email === user.email)) {
            return { success: false, message: "Email is already registered"};
        }
        users.push(user);
        await saveData("users", users);
        return { success: true};
    }
    catch (e) {
        console.error("Error registering user:", e);
        return{ success: false, message: "Error registering"}
    }
};

export const loginUser = async (email: string, password: string) => {
    try {
    const users: User[] = (await getData("users")) || [];
    const user = users.find(u => u.email === email && u.password === password);
    if (user) {
      await saveData("currentUser", user);
      return { success: true };
    } else {
      return { success: false, message: "Invalid credentials. Please try again." };
    }
  } catch (e) {
    console.error("Error logging in:", e);
    return { success: false, message: "Error logging in" };
  }
};

//Logout section 
export const logoutUser = async () => {
    try {
        await removeData("currentUser");
    }catch (e) {
        console.error("Error logging out", e);
    }
};

//Get current logged in user
export const getCurrentUser = async () => {
    return await getData("currentUser");
};