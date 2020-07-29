namespace Utils
{
    using System.Reflection;
    using System.Windows.Forms;

    public static class UpdateGUI
    {
        private delegate void UpdateControlDelegate(Control control, string propertyName, object propertyValue);

        public static void UpdateControl(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new UpdateControlDelegate
                (UpdateControl),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }
    }
}
