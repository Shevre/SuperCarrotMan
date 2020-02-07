using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarrotManv2.Core
{
    public class EventsHandler
    {
        List<EventObject> EventObjects = new List<EventObject>();
        public List<AreaEventObject> areaEventObjects { private set; get; } = new List<AreaEventObject>();
        public EventsHandler()
        { 
        }

        public void addEventObject(EventObject eventObject) 
        {
            EventObjects.Add(eventObject);
            
            
        }
        public void addEventObject(AreaEventObject eventObject)
        {
            EventObjects.Add(eventObject);
            areaEventObjects.Add(eventObject);
        }
        public void addEventObject(List<EventObject> eventObjects)
        {
            eventObjects.AddRange(eventObjects);
        }

        public void Update(Scene scene) 
        {
            foreach(EventObject e in EventObjects)
            {
                switch (e.Type)
                { 
                    case EventObjectTypes.AreaEvent:
                        ((AreaEventObject)e).Update(scene.Player);
                        break;
                    default:
                        e.Update();
                        break;
                }

            }
        }

        
    }
}
