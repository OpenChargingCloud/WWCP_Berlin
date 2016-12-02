using System;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace WWCP_Berlin
{

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
