using UnityEngine;

namespace Assets.Resources.Classes.Theme
{
    public class BackgroundThemeKnight : BackgroundTheme
    {
        private const string LAYER0 = "Painted HQ 2D Forest Medieval Background/day/mountain01";
        private const string LAYER1 = "Painted HQ 2D Forest Medieval Background/day/mountain02";
        private const string LAYER2 = "Painted HQ 2D Forest Medieval Background/day/cloud03";
        private const string LAYER3 = "Painted HQ 2D Forest Medieval Background/day/cloud02";
        private const string LAYER4 = "Painted HQ 2D Forest Medieval Background/day/sky";


        public BackgroundThemeKnight()
        {
            this.Layer0 = UnityEngine.Resources.Load<Sprite>(LAYER0);
            this.Layer1 = UnityEngine.Resources.Load<Sprite>(LAYER1);
            this.Layer2 = UnityEngine.Resources.Load<Sprite>(LAYER2);
            this.Layer3 = UnityEngine.Resources.Load<Sprite>(LAYER3);
            this.Layer4 = UnityEngine.Resources.Load<Sprite>(LAYER4);
        }
    }
}
