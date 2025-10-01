using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class DroneSample
    {
        [DataMember]
        public int drone_id {  get; set; }

        [DataMember]
        public int row {  get; set; }

        [DataMember]
        public float time { get; set; }

        [DataMember]
        public float wind_speed { get; set; }

        [DataMember]
        public float wind_angle { get; set; }

        [DataMember]
        public float battery_v { get; set; }

        [DataMember]
        public float battery_c { get; set; }

        [DataMember]
        public float pos_x { get; set; }

        [DataMember]
        public float pos_y { get; set; }

        [DataMember]
        public float pos_z { get; set; }

        [DataMember]
        public float ori_x { get; set; }

        [DataMember]
        public float ori_y { get; set; }

        [DataMember]
        public float ori_z { get; set; }

        [DataMember]
        public float ori_w { get; set; }

        [DataMember]
        public float vel_x { get; set; }

        [DataMember]
        public float vel_y { get; set; }

        [DataMember]
        public float vel_z { get; set; }

        [DataMember]
        public float lin_x { get; set; }

        [DataMember]
        public float lin_y { get; set; }

        [DataMember]
        public float lin_z { get; set; }

        [DataMember]
        public float ang_x { get; set; }

        [DataMember]
        public float ang_y { get; set; }

        [DataMember]
        public float ang_z { get; set; }



        public DroneSample() { }

        public DroneSample(
            int drone_id,
            int row,
            string time,
            string wind_speed,
            string wind_angle,
            string battery_v,
            string battery_c,
            string pos_x,
            string pos_y,
            string pos_z,
            string ori_x,
            string ori_y,
            string ori_z,
            string ori_w,
            string vel_x,
            string vel_y,
            string vel_z,
            string lin_x,
            string lin_y,
            string lin_z,
            string ang_x,
            string ang_y,
            string ang_z
            )
        {
            this.time = float.Parse(time, CultureInfo.InvariantCulture);
            this.wind_speed = float.Parse(wind_speed, CultureInfo.InvariantCulture);
            this.wind_angle = float.Parse(wind_angle, CultureInfo.InvariantCulture);
            this.battery_v = float.Parse(battery_v, CultureInfo.InvariantCulture);
            this.battery_c = float.Parse(battery_c, CultureInfo.InvariantCulture);
            this.pos_x = float.Parse(pos_x, CultureInfo.InvariantCulture);
            this.pos_y = float.Parse(pos_y, CultureInfo.InvariantCulture);
            this.pos_z = float.Parse(pos_z, CultureInfo.InvariantCulture);
            this.ori_x = float.Parse(ori_x, CultureInfo.InvariantCulture);
            this.ori_y = float.Parse(ori_y, CultureInfo.InvariantCulture);
            this.ori_z = float.Parse(ori_z, CultureInfo.InvariantCulture);
            this.ori_w = float.Parse(ori_w, CultureInfo.InvariantCulture);
            this.vel_x = float.Parse(vel_x, CultureInfo.InvariantCulture);
            this.vel_y = float.Parse(vel_y, CultureInfo.InvariantCulture);
            this.vel_z = float.Parse(vel_z, CultureInfo.InvariantCulture);
            this.lin_x = float.Parse(vel_x, CultureInfo.InvariantCulture);
            this.lin_y = float.Parse(vel_y, CultureInfo.InvariantCulture);
            this.lin_z = float.Parse(vel_z, CultureInfo.InvariantCulture);
            this.ang_x = float.Parse(vel_x, CultureInfo.InvariantCulture);
            this.ang_y = float.Parse(vel_y, CultureInfo.InvariantCulture);
            this.ang_z = float.Parse(vel_z, CultureInfo.InvariantCulture);
        }

        public ActionResult Validate()
        {
            // FALI KOD
            return new ActionResult(Result.SUCCESS);
        }
    }
}
