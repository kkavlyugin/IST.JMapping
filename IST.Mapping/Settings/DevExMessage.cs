using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace IST.JMapping.Settings
{
    public static class DevExMessage
    {
        public static void MessageBoxError(string text, string caption)
        {
            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void MessageBoxError(string text)
        {
            XtraMessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MessageBoxInformation(string text)
        {
            XtraMessageBox.Show(text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MessageBoxWarning(string text)
        {
            XtraMessageBox.Show(text, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static void MessageBoxWarning(string text, string caption)
        {
            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static void MessageBoxStop(string text)
        {
            XtraMessageBox.Show(text, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public static DialogResult MessageBoxQuestionYesNo(string text)
        {
            return XtraMessageBox.Show(text, "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult MessageBoxQuestionYesNoCancel(string text)
        {
            return XtraMessageBox.Show(text, "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static DialogResult MessageBoxQuestionYesNoCancel(string text, string caption)
        {
            return XtraMessageBox.Show(text, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
