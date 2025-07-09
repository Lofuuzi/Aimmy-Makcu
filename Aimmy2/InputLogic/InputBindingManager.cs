using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using MouseMovementLibraries.MakcuSupport;
using System.Windows.Threading;

namespace InputLogic
{
    internal class InputBindingManager
    {
        private IKeyboardMouseEvents? _mEvents;
        private readonly Dictionary<string, string> bindings = [];
        private static readonly Dictionary<string, bool> isHolding = [];
        private string? settingBindingId = null;

        public event Action<string, string>? OnBindingSet;

        private const string MakcuButtonPrefix = "Makcu_";

        public event Action<string>? OnBindingPressed;

        public event Action<string>? OnBindingReleased;

        public static bool IsHoldingBinding(string bindingId) => isHolding.TryGetValue(bindingId, out bool holding) && holding;

        public void SetupDefault(string bindingId, string keyCode)
        {
            bindings[bindingId] = keyCode;
            isHolding[bindingId] = false;
            OnBindingSet?.Invoke(bindingId, keyCode);
            EnsureHookEvents();
        }

        public void SetupMakcuEvents()
        {
            if (MakcuMain.MakcuInstance != null && MakcuMain.MakcuInstance.IsInitializedAndConnected)
            {
                MakcuMain.MakcuInstance.ButtonStateChanged -= MakcuMouseButtonStateChanged;
                MakcuMain.MakcuInstance.ButtonStateChanged += MakcuMouseButtonStateChanged;
                if (_mEvents != null)
                {
                    _mEvents.MouseDown -= GlobalHookMouseDown!;
                    _mEvents.MouseUp -= GlobalHookMouseUp!;
                }
            }
        }

        public void RestoreMouseEvents()
        {
            if (_mEvents != null)
            {
                _mEvents.MouseDown -= GlobalHookMouseDown!;
                _mEvents.MouseUp -= GlobalHookMouseUp!;
                _mEvents.MouseDown += GlobalHookMouseDown!;
                _mEvents.MouseUp += GlobalHookMouseUp!;
            }
        }



        public void StartListeningForBinding(string bindingId)
        {
            settingBindingId = bindingId;
            EnsureHookEvents();
        }

        private void EnsureHookEvents()
        {
            if (_mEvents == null)
            {
                _mEvents = Hook.GlobalEvents();
                _mEvents.KeyDown += GlobalHookKeyDown!;
                _mEvents.MouseDown += GlobalHookMouseDown!;
                _mEvents.KeyUp += GlobalHookKeyUp!;
                _mEvents.MouseUp += GlobalHookMouseUp!;
            }

            SetupMakcuEvents();
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            string keyCodeStr = e.KeyCode.ToString();
            if (settingBindingId != null)
            {
                bindings[settingBindingId] = e.KeyCode.ToString();
                OnBindingSet?.Invoke(settingBindingId, e.KeyCode.ToString());
                bindings[settingBindingId] = keyCodeStr;
                isHolding[settingBindingId] = false;
                OnBindingSet?.Invoke(settingBindingId, keyCodeStr);
                settingBindingId = null;
            }
            else
            {
                foreach (var binding in bindings)
                    foreach (var bindingEntry in bindings)
                    {
                    if (binding.Value == e.KeyCode.ToString())
                            if (bindingEntry.Value == keyCodeStr)
                            {
                        isHolding[binding.Key] = true;
                        OnBindingPressed?.Invoke(binding.Key);
                        isHolding[bindingEntry.Key] = true;
                        OnBindingPressed?.Invoke(bindingEntry.Key);
                         }
                }
            }
        }

        private void GlobalHookMouseDown(object sender, MouseEventArgs e)
        {
            string buttonCodeStr = e.Button.ToString();
            if (settingBindingId != null)
            {
                bindings[settingBindingId] = e.Button.ToString();
                OnBindingSet?.Invoke(settingBindingId, e.Button.ToString());
                settingBindingId = null;
                bindings[settingBindingId] = buttonCodeStr;
                isHolding[settingBindingId] = false;
                OnBindingSet?.Invoke(settingBindingId, buttonCodeStr);
            }
            else
            {
                foreach (var binding in bindings)
                foreach (var bindingEntry in bindings)
                    {
                    if (binding.Value == e.Button.ToString())
                            if (bindingEntry.Value == buttonCodeStr)
                            {
                        isHolding[binding.Key] = true;
                        OnBindingPressed?.Invoke(binding.Key);
                                isHolding[bindingEntry.Key] = true;
                                OnBindingPressed?.Invoke(bindingEntry.Key);
                            }
                }
            }
        }

        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var binding in bindings)
            {
                if (binding.Value == e.KeyCode.ToString())
                {
                    isHolding[binding.Key] = false;
                    OnBindingReleased?.Invoke(binding.Key);
                }
            }
        }

        private void GlobalHookMouseUp(object sender, MouseEventArgs e)
        {
            foreach (var binding in bindings)
            {
                if (binding.Value == e.Button.ToString())
                {
                    isHolding[binding.Key] = false;
                    OnBindingReleased?.Invoke(binding.Key);
                }
            }
        }

        public void StopListening()
        {
            if (_mEvents != null)
            {
                _mEvents.KeyDown -= GlobalHookKeyDown!;
                _mEvents.MouseDown -= GlobalHookMouseDown!;
                _mEvents.KeyUp -= GlobalHookKeyUp!;
                _mEvents.MouseUp -= GlobalHookMouseUp!;
                _mEvents.Dispose();
                _mEvents = null;

                if (MakcuMain.MakcuInstance != null)
                {
                    try
                    {
                        MakcuMain.MakcuInstance.ButtonStateChanged -= MakcuMouseButtonStateChanged;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"DEBUG: Error MakcuMouse: {ex.Message}");
                    }
                }
            }
        }

        private void MakcuMouseButtonStateChanged(MakcuMouseButton button, bool isPressed)
        {
            string makcuButtonCodeStr = button.ToString();

            if (settingBindingId != null && isPressed)
            {

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    bindings[settingBindingId] = makcuButtonCodeStr;
                    isHolding[settingBindingId] = false;
                    OnBindingSet?.Invoke(settingBindingId, makcuButtonCodeStr);
                    settingBindingId = null;
                });
            }
            else
            {
                foreach (var bindingEntry in bindings)
                {
                    if (bindingEntry.Value == makcuButtonCodeStr)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            isHolding[bindingEntry.Key] = isPressed;
                            if (isPressed)
                                OnBindingPressed?.Invoke(bindingEntry.Key);
                            else
                                OnBindingReleased?.Invoke(bindingEntry.Key);
                        });

                    }
                }
            }
        }


    }
}