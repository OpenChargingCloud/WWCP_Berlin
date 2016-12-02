using System;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace WWCP_Berlin
{

    // https://services.mobilitaetsdienste.de/viz/production/wms/2/wms_list/?lang=de&CATEGORY=fuelstation&BBOX=13.30280660766607,52.48432862608622,13.51532339233404,52.58936825790365

    public static class BerlinStations
    {

        public static JObject ParseJSON(String Text)
        {

            var BerlinJSON = JObject.Parse(Text);

            return new JObject(
                       new JProperty("type",       "FeatureCollection"),
                       new JProperty("properties", new JObject()),
                       new JProperty("features",   new JArray(
                           BerlinJSON["list"].Select(item => {

                               var InfoObject = item as JObject;

                               return new JObject(
                                          new JProperty("type", "Feature"),
                                          new JProperty("properties", new JObject(
                                              new JProperty("Id",       InfoObject["pointId"]),
                                              new JProperty("Operator", InfoObject["operatorId"])
                                          )),
                                          new JProperty("geometry", new JObject(
                                              new JProperty("type", "Point"),
                                              new JProperty("coordinates", new JArray(InfoObject["x"], InfoObject["y"]))
                                          ))

                                   );

                           }
                       )))
                   );

        }

    }

}
