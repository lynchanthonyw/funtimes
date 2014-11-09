using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkingAround.Game.Actions
{
    public class DialogAction : TaskAction
    {
        private string Dialog { get; set; }


        public DialogAction(string dialog, VisualObject source = null, VisualObject target = null, long duration = 0, long cooldown = 0)
            : base(source, target, duration, cooldown)
        {
            Dialog = dialog;
        }

        internal override bool Action()
        {
            try
            {
                Console.WriteLine(Dialog);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override TaskAction Clone()
        {
            throw new NotImplementedException();
        }
    }
}
