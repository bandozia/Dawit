using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dawit.Infrastructure.Service.Extensions;

namespace Dawit.Tests
{
    public class HashingExtensionsExecute
    {
        [Fact(DisplayName = "ToHashed encode string in fixed lengh")]
        public void ToHashedWorking()
        {
            string hashed = "thepassstring".ToHashed();
            Assert.Equal(48, hashed.Length);
        }

        [Fact(DisplayName = "IsEqualsHashed works")]        
        public void IsEqualsHashedWorks()
        {
            string hashed = "thepassstring".ToHashed();
            
            bool correct = "thepassstring".IsEqualsHashed(hashed);
            bool wrong = "notpassstring".IsEqualsHashed(hashed);

            Assert.True(correct);
            Assert.False(wrong);
        }
                
    }
}
