using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infection;

namespace AIV_Engine
{
    internal class JoypadController : Controller
    {
        public JoypadController(int ctrlIndex) : base(ctrlIndex)
        {

        }

        public override float GetHorizontal()
        {
            float direction = 0.0f;
            
            if(Game.Window.JoystickRight(index))
            {
                direction = 1.0f;
            }
            else if(Game.Window.JoystickLeft(index))
            {
                direction = -1.0f;
            }
            else
            {
                direction = Game.Window.JoystickAxisLeft(index).X;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction = 0.0f;

            if (Game.Window.JoystickDown(index))
            {
                direction = 1.0f;
            }
            else if (Game.Window.JoystickUp(index))
            {
                direction = -1.0f;
            }
            else
            {
                direction = Game.Window.JoystickAxisLeft(index).Y;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Window.JoystickX(index);
        }

        public override bool IsJumpPressed()
        {
            return Game.Window.JoystickY(index);
        }
    }
}
