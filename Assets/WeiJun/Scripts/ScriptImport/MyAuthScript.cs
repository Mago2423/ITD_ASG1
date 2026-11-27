using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.SceneManagement;
using System;
using Firebase;

public class MyAuthScript : MonoBehaviour
{
   public TMP_InputField email;
   public TMP_InputField password;
   public TMP_Text errorMessage;
   public GameObject Menu;


    public void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += AuthStateChangeHandler;
    }

    private void AuthStateChangeHandler(object sender, EventArgs e)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            var user = FirebaseAuth.DefaultInstance.CurrentUser;
            Debug.Log($"Current user is {user.Email} {user.UserId}");
        }
        else
        {
            Debug.Log("No user is signed in.");
        }
    }
    public bool CheckInputs()
    {
        errorMessage.text = "";
        
        if(email.text.Length ==0)
        {
            errorMessage.text = "Email cannot be empty.";
            return false;
        }
        else if(password.text.Length == 0)
        {
            errorMessage.text = "password cannot be empty.";
            return false    ;
        }
        else if(password.text.Length < 8)
        {
            errorMessage.text = "Password must be at least 8 characters long.";
            return false;
        }
        return true;
    }
    
   public void SignUp()
   {
       if (!CheckInputs())
           return;

        FirebaseAuth
            .DefaultInstance
            .CreateUserWithEmailAndPasswordAsync(email.text, password.text)
            .ContinueWithOnMainThread(task => 
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    var firebaseException = task.Exception.GetBaseException() as FirebaseException;
                    var authError = (AuthError)firebaseException.ErrorCode;
                    switch (authError)
                    {
                        case AuthError.EmailAlreadyInUse:
                            errorMessage.text = "Email already in use.";
                            break;
                        case AuthError.InvalidEmail:
                            errorMessage.text = "Invalid email address.";
                            break;

                        case AuthError.WeakPassword:
                            errorMessage.text = "Weak password. Password should be at least 6 characters long.";
                            break;
                        default:
                            errorMessage.text = "Sign up failed. Please try again.";
                            break;
                    }
                }
                else if (task.IsCanceled)
                {
                    Debug.Log("Sign up canceled.");
                    errorMessage.text = "Sign up canceled";
                }
                else
                {
                    var user = task.Result.User;
                    Debug.Log($"New User ID is {user.UserId}");

                    var player = new Player(email.text, "Noob", transform.position);
                    var json = JsonUtility.ToJson(player);

                    FirebaseDatabase
                        .DefaultInstance
                        .GetReference("players")
                        .Child(user.UserId)
                        .SetRawJsonValueAsync(json);
                }
            });
   }

    public void LogIn()
    {
        if (!CheckInputs())
           return;

        FirebaseAuth
            .DefaultInstance
            .SignInWithEmailAndPasswordAsync(email.text, password.text)
            .ContinueWithOnMainThread(task => 
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    var firebaseException = task.Exception.GetBaseException() as FirebaseException;
                    var authError = (AuthError)firebaseException.ErrorCode;
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            errorMessage.text = "Invalid email address.";
                            break;
                        case AuthError.WeakPassword:
                            errorMessage.text = "Weak password. Password should be at least 6 characters long.";
                            break;
                        default:
                            errorMessage.text = "Login failed. Please try again.";
                            break;
                    }
                }
                else if (task.IsCanceled)
                {
                    Debug.Log("Login canceled.");
                    errorMessage.text = "Login canceled";
                }
                else
                {
                    var user = task.Result.User;

                    FirebaseDatabase
                        .DefaultInstance
                        .GetReference("players")
                        .Child(user.UserId)
                        .GetValueAsync()
                        .ContinueWithOnMainThread(task => 
                        {
                            if (task.IsFaulted)
                            {
                                Debug.LogError(task.Exception);
                                var firebaseException = task.Exception.GetBaseException() as FirebaseException;
                                var authError = (AuthError)firebaseException.ErrorCode;
                                switch (authError)
                                {
                                    case AuthError.UserNotFound:
                                        errorMessage.text = "User not found.";
                                        break;
                                    case AuthError.WrongPassword:
                                        errorMessage.text = "Incorrect password.";
                                        break;
                                    case AuthError.UserDisabled:
                                        errorMessage.text = "User account has been disabled.";
                                        break;
                                    default:
                                        errorMessage.text = "Login failed. Please try again.";
                                        break;
                                }
                            }
                            else if (task.IsCanceled)
                            {
                                Debug.Log("Login canceled.");
                                errorMessage.text = "Login canceled";
                            }
                            else
                            {
                                var json = task.Result.GetRawJsonValue();
                                var player = JsonUtility.FromJson<Player>(json);
                                SceneManager.LoadScene("GameScene");
                            }
                        });
                }
            });
    }
    public void ForgotPassword()
    {
        errorMessage.text = "";
        if(email.text.Length == 0)
        {
            errorMessage.text = "Email cannot be empty.";
            return;
        }

        FirebaseAuth
        .DefaultInstance
        .SendPasswordResetEmailAsync(email.text)
            .ContinueWithOnMainThread(task => 
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    var firebaseException = task.Exception.GetBaseException() as FirebaseException;
                    var authError = (AuthError)firebaseException.ErrorCode;
                    switch (authError)
                    {
                        case AuthError.UserNotFound:
                            errorMessage.text = "User not found.";
                            break;
                        default:
                            errorMessage.text = "Failed to send password reset email. Please try again.";
                            break;
                    }
                }
                else if (task.IsCanceled)
                {
                    Debug.Log("Forgot password canceled.");
                    errorMessage.text = "Forgot password canceled";
                }
                else
                {
                    errorMessage.text = "Please check your email for reset password instructions.";
                }
            });
    }
    public void SignOut()
    {
        FirebaseAuth
        .DefaultInstance
        .SignOut();
        SceneManager.LoadScene("SampleScene");
    }
    public void OnMenu()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
}
