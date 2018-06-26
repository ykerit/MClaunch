using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MCLaunch.View
{
    class WaterMaskTextBox: TextBox
    {
        /// <summary>
        /// 水印文本
        /// </summary>
        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }
        /// <summary>
        /// 水印文本依赖属性
        /// </summary>
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintText",
                typeof(string),
                typeof(WaterMaskTextBox),
                new PropertyMetadata(string.Empty,
                    (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
                    {
                        var oldValue = Convert.ToString(e.OldValue);
                        var newValue = Convert.ToString(e.NewValue);
                        var sender = d as WaterMaskTextBox;
                        if (sender != null)
                        {
                            if (string.IsNullOrWhiteSpace(oldValue))
                            {
                                if (string.IsNullOrWhiteSpace(newValue))
                                {
                                    sender.GotFocus -= Sender_GotFocus;
                                    sender.LostFocus += Sender_LostFocus;
                                }
                                else
                                {
                                    sender.GotFocus += Sender_GotFocus;
                                    sender.LostFocus += Sender_LostFocus;
                                    sender.Text = sender.HintText;
                                }
                            }
                        }
                    }));
        public bool HasHintText
        {
            get { return (bool)GetValue(HasHintTextProperty); }
            set { SetValue(HasHintTextProperty, value); }
        }

        /// <summary>
        /// 是否有 水印提示文本 依赖属性
        /// </summary>
        public static readonly DependencyProperty HasHintTextProperty =
            DependencyProperty.Register("HasHintText", typeof(bool), typeof(WaterMaskTextBox), new PropertyMetadata(false));
        /// <summary>
        /// 当获得焦点是
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Sender_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as WaterMaskTextBox;
            if (textBox.Text == textBox.HintText)
            {
                textBox.Text = string.Empty;
            }
        }
        /// <summary>
        /// 当失去焦点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Sender_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as WaterMaskTextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.HintText;
            }
        }

    }
}

