using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WeightliftingManagment.Domain.Model;
using WeightliftingManagment.Moduls.ParticipantBodyWeight.Views;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.DesignTest
{
    public class ParticipantCollectionData
    {
        public static ParticipantCollection GetParticipantCollection()
        {
            var participantCollection = new ParticipantCollection {
                new Participant(1, "Paweł Gawęda", "KPC Hejnał Kęty", 105.4, 1986, Gender.Men, new Attempt(100), new Attempt(120), "A", 1.024125, "da5sd1fa",new Category("M 109")),
                new Participant(2, "Patrycja Gawęda", "KPC Hejnał Kęty", 54.1, 1991, Gender.Women, new Attempt(56), new Attempt(65), "A", 1.024125, "da5sd1fa",new Category("W 55"))
            };
            return participantCollection;
        }
    }
}
