namespace Utils.Models
{
    using System;

    [Serializable]
    public class User
    {
        public string UserName { get; set; }

        public string LabName { get; set; }

        public string TestName { get; set; }

        public User(string userName, string labName, string testName)
        {
            UserName = userName;

            LabName = labName;

            TestName = testName;
        }

        public void UpdateUserData(string userName, string labName, string testName)
        {
            UserName = userName;

            LabName = labName;

            TestName = testName;
        }
    }
}
