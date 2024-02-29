using App.DTO.SuperClasses;

namespace App.DTO.Abstracts
{
    public abstract class AnimOutFitBase : DtoModel{
        public string Name { get; set; } = default!;
        public long LooktypeEx { get; set; }
        public long Looktype { get; set; }
        public long Lookaddons { get; set; }
        public long Lookhead { get; set; }
        public long Lookbody { get; set; }
        public long Looklegs { get; set; }
        public long Lookfeet { get; set; }
        public int Lookmount { get; set; }

    }

    public abstract class AnimOutFit : AnimOutFitBase
    {
        public string LinkAnimOutFit { 
            get {
                return $"https://App.com/resources/images/charactertrade/outfits/animoutfit.php?id={Looktype}&addons={Lookaddons}&head={Lookhead}&body={Lookbody}&legs={Looklegs}&feet={Lookfeet}&mount={Lookmount}";
            }
        }
    }

    public class AnimOutFitBoostedResponse : AnimOutFitBase
    {
        public string LinkAnimOutFit
        {
            get
            {
                if (LooktypeEx == 0)
                    return $"https://App.com/resources/images/charactertrade/outfits/animoutfit.php?id={Looktype}&addons={Lookaddons}&head={Lookhead}&body={Lookbody}&legs={Looklegs}&feet={Lookfeet}&mount={Lookmount}";
                else
                    return $"https://App.com/resources/images/charactertrade/items/{LooktypeEx}.gif";
            }
        }
    }
}
