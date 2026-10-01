using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2020
{
    public static class Localization
    {
        public static string ToSpanish(string text)
        {
            switch(text)
            {
                case "Pants (Generic)" : return "Pantalones (Genéricos)";
                case "Top (Generic)" : return "Parte superior (Genérica)";
                case "Jacket (Generic)" : return "Chaqueta (Genérica)";
                case "Footwear (Generic)" : return "Calzado (Genérico)";
                case "Jewelry (Generic)" : return "Joyas (Genéricas)";
                case "Mirrorshades (Generic)" : return "Gafas de espejo (Genéricas)";
                case "Contact Lenses (Generic)" : return "Lentes de contacto (Genéricas)";
                case "Glasses (Generic)" : return "Gafas (Genéricas)";

                case "Techscanner" : return "Escáner tecnológico";
                case "Cutting Torch" : return "Soplete";
                case "Tech Toolkit" : return "Kit de herramientas";
                case "B&E Tools" : return "Herramientas de Ladrón";
                case "Electronic Toolkit" : return "Kit de herramientas electrónicas";
                case "Protective Googles" : return "Gafas protectoras";
                case "Flashtube" : return "Linterna";
                case "Glowstick" : return "Bastón Luminoso";
                case "Paint, ltr" : return "Pintura, litro";
                case "Flash Tape, mtr" : return "Cinta reflectante, metro";
                case "Rope, mtr" : return "Cuerda, metro";
                case "Breathing Mask" : return "Máscara respiratoria";

                case "Video Board, mtr2" : return "Panel de vídeo, m²";
                case "Data Chip" : return "Chip de datos";
                case "Logcompass" : return "Brújula digital";
                case "Digital Recorder" : return "Grabadora digital";
                case "Digital Camera" : return "Cámara digital";
                case "VideoCam" : return "Videocámara";
                case "V/A Tape Player" : return "Reproductor de vídeo/audio";
                case "Videotape" : return "Cinta de vídeo";
                case "Pocket TV" : return "Televisor de bolsillo";
                case "Digital Chip Player" : return "Reproductor de chips digitales";
                case "Digital Music Chip" : return "Chip de música digital";
                case "Electric Guitar" : return "Guitarra Eléctrica";
                case "Electric Keyboard" : return "Teclado Electrónico";
                case "Drum Synth" : return "Sintetizador de Percusión";
                case "Amplifier" : return "Amplificador";

                case "Ammo, Light Pistol/SMG (100)" : return "Munición, pistola lig/subfusil (100)";
                case "Ammo, Medium Pistol/SMG (50)" : return "Munición, pistola media/subfusil (50)";
                case "Ammo, Heavy Pistol/SMG (50)" : return "Munición, pistola pes/subfusil (50)";
                case "Ammo, Very Heavy Pistol (50)" : return "Munición, pistola muy pesada (50)";
                case "Ammo, Assault Rifle (100)" : return "Munición, fusil (100)";
                case "Ammo, Shotgun (12)" : return "Munición, escopeta (12)";
                case "Ammo, 20mm round (1)" : return "Munición, proyectil 20 mm (1)";
                case "Ammo, Std. Arrows (12)" : return "Munición, flechas (12)";
                case "Ammo, Std. Crossbow bolts (12)" : return "Munición, virotes estándar (12)";
                case "Ammo, Airgun pellets (100)" : return "Munición, perdigones (100)";
                case "Ammo, Needlegun rounds (50)" : return "Munición, pistola de agujas (50)";
                case "Ammo, Flamethrower (1)" : return "Munición, lanzallamas (1)";
                case "Ammo, Std. Micro Missile (4)" : return "Munición, micromisiles estándar (4)";
                case "Ammo, AP Light Pistol/SMG (100)" : return "Munición, perforante PL/subfusil (100)";
                case "Ammo, Needlegun rounds, acid/drug (50)" : return "Munición, pistola de agujas, ácido/droga (50)";

                case "Silencer" : return "Silenciador";
                case "Holster, any" : return "Funda, cualquiera";
                case "Shoulder sling" : return "Correa de hombro";
                case "Pistol Laser Pointer (+1WA)" : return "Puntero láser para pistola (+1PREC)";

                case "Heavy Leather Jacket" : return "Chaqueta de cuero grueso";
                case "Heavy Leather Pants" : return "Pantalones de cuero grueso";
                case "Kevlar Vest" : return "Chaleco de Kevlar";
                case "Corp Militar Body Armor" : return "Armadura corporal militar corporativa";

                case "Laptop" : return "Ordenador portátil";
                case "Pocket Computer" : return "Ordenador de bolsillo";
                case "Interface Cables" : return "Cables de interfaz";
                case "Low Impedance Cables" : return "Cables de baja impedancia";
                case "Trode Set" : return "Juego de trodos";
                case "Keyboard (Computer)" : return "Teclado (ordenador)";
                case "Terminal (Full PC)" : return "Terminal (PC completo)";

                case "Mastoid Communicator" : return "Comunicador mastoideo";
                case "Pocket Communicator" : return "Comunicador de bolsillo";
                case "Cellphone" : return "Teléfono móvil";
                case "Mini Cellphone" : return "Mini teléfono móvil";

                case "Binoglasses" : return "Binogafas";
                case "Binoculars" : return "Prismáticos";
                case "Light Booster Googles" : return "Gafas de Visión Nocturna";
                case "IR Googles" : return "Gafas IR";
                case "IR/UV Flash" : return "Linterna IR/UV";

                case "Movie" : return "Cine";
                case "Chip Rental" : return "Alquiler de chip";
                case "Braindance" : return "Braindance";
                case "Live/Sports Event" : return "Evento en directo/deportivo";
                case "Fast Food" : return "Comida rápida";

                case "Keylock, per Level" : return "Cerradura de llave, por nivel";
                case "Cardlock, per level" : return "Cerradura de tarjeta, por nivel";
                case "Vocolock, per level" : return "Cerradura de voz, por nivel";
                case "Line Tap Bug" : return "Pinchalíneas";
                case "Code Decryptor" : return "Descifrador de códigos";
                case "Voc Decryptor" : return "Descifrador de voz";
                case "Security Scanner" : return "Escáner de seguridad";
                case "Poison Sniffer" : return "Detector de veneno";
                case "Jamming Transmitter" : return "Transmisor de interferencias";
                case "Scanner Plate" : return "Placa de escáner";
                case "Movement Sensor" : return "Sensor de movimiento";
                case "Passcard" : return "Tarjeta de acceso";
                case "Tracking Device" : return "Dispositivo de seguimiento";
                case "Tracer Button" : return "Botón rastreador";
                case "Remote Sensors" : return "Sensores remotos";
                case "PlasKuffs" : return "Esposas de Nylon";
                case "Stripwire Binders" : return "Bandas Adhesivas";

                case "Dermal Stapler" : return "Grapadora dérmica";
                case "Spray Skin, per can" : return "Piel en aerosol, lata";
                case "Cryotank" : return "Criotanque";
                case "Medikit" : return "Botiquín médico";
                case "Surgical Kit" : return "Kit quirúrgico";
                case "First Aid Kit" : return "Botiquín de primeros auxilios";
                case "Medscanner" : return "Escáner médico";
                case "Drug Analyser" : return "Analizador de drogas";
                case "Airhypo" : return "Jeringa de Aire Comprimido";

                case "Nylon Carrybag" : return "Bolsa de transporte de nailon";
                case "Sleeping Bag" : return "Saco de dormir";
                case "Inflatable Bed" : return "Cama hinchable";
                case "Futon" : return "Futón";
                case "Real Wood Furniture, per piece" : return "Muebles de madera auténtica, pieza";
                case "Synthetic Furniture, per piece" : return "Muebles sintéticos, pieza";
                case "Apartment Cube" : return "Cubo de apartamento";
                case "Lamp" : return "Lámpara";
                case "Cleaning Bot" : return "Robot de limpieza";
                case "Vocal Switcher System" : return "Sistema de cambio de voz";

                case "Scooter, Generic" : return "Scooter, genérico";
                case "Scooter, Generic, CC" : return "Scooter, genérico, CC";
                case "Motorcycle, Generic" : return "Motocicleta, genérica";
                case "Motorcycle, Generic, CC" : return "Motocicleta, genérica, CC";
                case "KeiCar, Generic" : return "Kei car, genérico";
                case "KeiCar, Generic, CC" : return "Kei car, genérico, CC";
                case "Small Compact, Generic" : return "Coche compacto pequeño, genérico";
                case "Small Compact, Generic, CC" : return "Coche compacto pequeño, genérico, CC";
                case "Sedan, Generic" : return "Sedán, genérico";
                case "Sedan, Generic, CC" : return "Sedán, genérico, CC";
                case "Sports Car, Generic" : return "Coche deportivo, genérico";
                case "Sports Car, Generic, CC" : return "Coche deportivo, genérico, CC";
                case "Luxury Sedan, Generic" : return "Sedán de lujo, genérico";
                case "Luxury Sedan, Generic, CC" : return "Sedán de lujo, genérico, CC";

                case "Cellphone Service, Month" : return "Servicio de teléfono móvil, mes";
                case "Standard Phone Service, Month" : return "Servicio telefónico estándar, mes";
                case "CredChip Account, Month" : return "Cuenta CredChip, mes";
                case "Health Plan, Month" : return "Plan de salud, mes";
                case "Trauma Team Acct, Month" : return "Cuenta de Trauma Team, mes";
                case "Cable TV" : return "Televisión por cable";

                case "Kibble, per Week" : return "Friskies, por semana";
                case "Basic Prepak, per Week" : return "Comida precocinada básica, por semana";
                case "Good Prepak, per Week" : return "Comida precocinada buena, por semana";
                case "Fresh Food, per Week" : return "Comida fresca, por semana";

                default: return "Unknown string, review localization";
            }
    }
    }
}
