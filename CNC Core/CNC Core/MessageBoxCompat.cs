/*
 * MessageBoxCompat.cs - Cross-platform MessageBox compatibility layer
 * Part of CNC Controls library for cross-platform Avalonia support
 */

using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace CNC.Core
{
    /// <summary>
    /// Cross-platform MessageBox implementation for Avalonia compatibility
    /// Temporary implementation using Debug output
    /// </summary>
    public static class MessageBox
    {
        public static MessageBoxResult Show(string messageBoxText)
        {
            Debug.WriteLine($"MessageBox: {messageBoxText}");
            return MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            Debug.WriteLine($"MessageBox [{caption}]: {messageBoxText}");
            return MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            Debug.WriteLine($"MessageBox [{caption}] ({button}): {messageBoxText}");
            return button == MessageBoxButton.YesNo ? MessageBoxResult.Yes : MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            Debug.WriteLine($"MessageBox [{caption}] ({button}, {icon}): {messageBoxText}");
            return button == MessageBoxButton.YesNo ? MessageBoxResult.Yes : MessageBoxResult.OK;
        }
    }

    /// <summary>
    /// Cross-platform resource access helper for Avalonia compatibility
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Find resource in current control or application resources
        /// </summary>
        public static object FindResource(Control control, string resourceKey)
        {
            try
            {
                if (control?.TryFindResource(resourceKey, out var resource) == true)
                    return resource;
                
                if (Application.Current?.TryFindResource(resourceKey, out resource) == true)
                    return resource;
                
                Debug.WriteLine($"Resource not found: {resourceKey}");
                return resourceKey; // Return key as fallback
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error finding resource {resourceKey}: {ex.Message}");
                return resourceKey;
            }
        }
    }

    /// <summary>
    /// Extension methods for cross-platform compatibility
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Cross-platform FindResource method for UserControl compatibility
        /// </summary>
        public static object FindResource(this Control control, string resourceKey)
        {
            return ResourceHelper.FindResource(control, resourceKey);
        }
    }

    /// <summary>
    /// MessageBox button types for cross-platform compatibility
    /// </summary>
    public enum MessageBoxButton
    {
        OK,
        OKCancel,
        YesNoCancel,
        YesNo
    }

    /// <summary>
    /// MessageBox result types for cross-platform compatibility
    /// </summary>
    public enum MessageBoxResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    /// <summary>
    /// MessageBox icon types for cross-platform compatibility
    /// </summary>
    public enum MessageBoxImage
    {
        None,
        Hand,
        Question,
        Exclamation,
        Asterisk,
        Stop,
        Error,
        Warning,
        Information
    }

    /// <summary>
    /// WPF Visibility compatibility for Avalonia
    /// </summary>
    public enum Visibility
    {
        Visible,
        Hidden,
        Collapsed
    }

    /// <summary>
    /// Extension methods for Visibility conversion
    /// </summary>
    public static class VisibilityExtensions
    {
        /// <summary>
        /// Convert WPF Visibility to Avalonia IsVisible boolean
        /// </summary>
        public static bool ToIsVisible(this Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }

        /// <summary>
        /// Convert boolean to WPF Visibility enum
        /// </summary>
        public static Visibility ToVisibility(this bool isVisible)
        {
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    /// <summary>
    /// Cross-platform Keyboard compatibility for Avalonia
    /// </summary>
    public static class Keyboard
    {
        /// <summary>
        /// Get current keyboard modifiers (simplified implementation)
        /// In a real implementation, this would need to track state or access through TopLevel
        /// </summary>
        public static KeyModifiers Modifiers
        {
            get
            {
                try
                {
                    // In Avalonia, we need access to the current TopLevel (Window)
                    // This is a simplified fallback - in practice, modifiers should be passed through events
                    return KeyModifiers.None;
                }
                catch
                {
                    return KeyModifiers.None;
                }
            }
        }
    }
}