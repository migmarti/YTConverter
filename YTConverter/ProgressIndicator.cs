using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTConverter
{

    class ProgressIndicator
    {
        private ProgressBar progressBar;
        private Label percentLabel;

        public ProgressIndicator(ProgressBar progressBar, Label percentLabel)
        {
            this.progressBar = progressBar;
            this.percentLabel = percentLabel;
        }

        public void updateProgress(string text, int percent)
        {
            if (ControlInvokeRequired(progressBar, () => updateProgress(text, percent))) return;
            if (ControlInvokeRequired(percentLabel, () => updateProgress(text, percent))) return;
            progressBar.Value = percent;
            percentLabel.Text = text + percent + "%";
        }

        private static bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;
            return true;
        }
    }
}
