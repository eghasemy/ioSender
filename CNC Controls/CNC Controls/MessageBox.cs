/*
 * MessageBox.cs - MessageBox compatibility layer for Avalonia
 *
 * v0.46 / 2025-01-15 / Io Engineering (Terje Io)
 */

using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace CNC.Controls
{
    public enum MessageBoxButton
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public enum MessageBoxImage
    {
        None,
        Hand,
        Question,
        Exclamation,
        Asterisk,
        Warning,
        Error,
        Information
    }

    public enum MessageBoxResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public static class MessageBox
    {
        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(messageBoxText, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(messageBoxText, caption, button, icon, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            // Simple implementation for now - more sophisticated implementation would use MsBox.Avalonia
            // For now, always return the default or appropriate result to avoid blocking
            
            switch (button)
            {
                case MessageBoxButton.YesNo:
                    return defaultResult != MessageBoxResult.None ? defaultResult : MessageBoxResult.Yes;
                case MessageBoxButton.YesNoCancel:
                    return defaultResult != MessageBoxResult.None ? defaultResult : MessageBoxResult.Yes;
                case MessageBoxButton.OKCancel:
                    return defaultResult != MessageBoxResult.None ? defaultResult : MessageBoxResult.OK;
                default:
                    return MessageBoxResult.OK;
            }
        }
    }
}