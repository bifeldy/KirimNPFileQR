/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Text Pemisah QR Baris Kolom
 *              :: Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

using System;

namespace KirimNPFileQR.Models {
    
    public sealed class TextDevider {

        private int lowerBound = 0;
        public int LowerBound {
            get {
                return lowerBound;
            }
        }

        private int maxCharacter = 0;
        public int MaxCharacter {
            get {
                return maxCharacter;
            }
            set {
                maxCharacter = value;
                lowerBound = GetLowerBound();
                arrDevidedText = new string[lowerBound];
            }
        }

        private string[] arrDevidedText = null;
        public string[] ArrDevidedText {
            get {
                return arrDevidedText;
            }
            set {
                arrDevidedText = value;
            }
        }

        public int JumlahPart {
            get {
                return arrDevidedText.Length;
            }
        }

        private string text = null;

        public TextDevider(string dataText) {
            text = dataText;
            maxCharacter = 250;
            lowerBound = GetLowerBound();
            arrDevidedText = new string[lowerBound];
        }

        public TextDevider(string dataText, int maxChar) {
            text = dataText;
            maxCharacter = maxChar;
            lowerBound = GetLowerBound();
            arrDevidedText = new string[lowerBound];
        }

        private int GetLowerBound() {
            int dataTextLength = text.Length;
            int lowerBound = (int)Math.Ceiling(dataTextLength * 1.0 / maxCharacter);
            return lowerBound;
        }

        public void Devide() {
            for (int i = 0; i < lowerBound; i++) {
                int startingIndex = (i * maxCharacter);
                if (i == lowerBound - 1) {
                    arrDevidedText[i] = text.Substring(startingIndex, text.Length % maxCharacter);
                }
                else {
                    arrDevidedText[i] = text.Substring(startingIndex, maxCharacter);
                }
            }
        }

    }
}
