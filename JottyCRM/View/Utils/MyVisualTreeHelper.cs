using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace JottyCRM.View.Utils
{
    public static class MyVisualTreeHelper
    {
        static bool AlwaysTrue<T>(T obj) { return true; }
        
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            return FindParent<T>(child, AlwaysTrue<T>);
        }

        public static T FindParent<T>(DependencyObject child, Predicate<T> predicate) where T : DependencyObject
        {
            DependencyObject parent = GetParent(child);
            if (parent == null)
                return null;

            if ((parent is T) && (predicate((T)parent)))
                return parent as T;
            else
                return FindParent<T>(parent);
        }

        static DependencyObject GetParent(DependencyObject child)
        {
            DependencyObject parent = null;
            if (child is Visual || child is Visual3D)
                parent = VisualTreeHelper.GetParent(child);

            return parent ?? LogicalTreeHelper.GetParent(child);
        }
    }
}