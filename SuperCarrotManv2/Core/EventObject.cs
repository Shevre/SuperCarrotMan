using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public enum EventObjectTypes { Generic,AreaEvent}
    public class EventObject
    {
        public delegate void EventTriggeredEventHandler();
        public event EventTriggeredEventHandler EventTriggered;
        public EventObjectTypes Type { private set; get; } = EventObjectTypes.Generic;
        public EventObject() 
        {
            
        }
        public EventObject(EventObjectTypes type)
        {
            Type = type;
        }

        public void Invoke() 
        {
            OnEventTriggered();
        }

        public void Update() 
        {
            
        }

        protected virtual void OnEventTriggered()
        {
            EventTriggered?.Invoke();
        }
    }

    public class AreaEventObject : EventObject
    {
        public VecRectangle BoundingBox { private set; get; }
        Vector2 Position;
        public delegate void PlayerLeftEventHandler();
        public event PlayerLeftEventHandler PlayerLeft;
        public AreaEventObject(Vector2 position,Vector2 boundingBox) : base(EventObjectTypes.AreaEvent)
        {
            Position = position;
            BoundingBox = new VecRectangle(position, boundingBox);
        }
        bool once = false;
        public void Update(Entities.Player player)
        {
            if (player.GetVecRectangle().Intersects(BoundingBox))
            {
                Invoke();
                once = true;
            }
            else
            {
                if (once)
                {
                    OnPlayerLeft();
                    once = false;
                }
            }
        }

        public void OnPlayerLeft()
        {
            PlayerLeft?.Invoke();
        }
    }
}
