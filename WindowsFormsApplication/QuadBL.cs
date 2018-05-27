using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApplication {
  public class QuadBL {
        public static void SerializeList ( List<Quad> figures, string path ) {
            XmlSerializer formatter = new XmlSerializer( typeof( List<Quad> ) );
            using( FileStream fs = new FileStream( path, FileMode.OpenOrCreate ) ) {
                formatter.Serialize( fs, figures );
            }
        }

        public static List<Quad> DeserializeList ( string path ) {
            XmlSerializer formatter = new XmlSerializer( typeof( List<Quad> ) );
            List<Quad> figures = null;
            using( FileStream fs = new FileStream( path, FileMode.Open ) ) {
                figures = (List<Quad>) formatter.Deserialize( fs );
            }

            if( figures == null ) {
                throw new ApplicationException( string.Format( $"can't deserialize file {path}" ) );
            }

            return figures;
        }
    }
}
