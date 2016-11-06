﻿#region Mr. Advice
// Mr. Advice
// A simple post build weaving package
// http://mradvice.arxone.com/
// Released under MIT license http://opensource.org/licenses/mit-license.php
#endregion

namespace MethodLevelTest
{
    using System;
    using ArxOne.MrAdvice.Advice;
    using ArxOne.MrAdvice.Annotation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PointcutRulesTest
    {
        [IncludePointcut("*.Set@")]
        public class SetterAdvice : Attribute, IMethodAdvice
        {
            public void Advise(MethodAdviceContext context)
            {
                context.Proceed();
            }
        }

        [SetterAdvice]
        public class SetterAdvisedType
        {
            public void SetA() { }
            public void ZetA() { }
            public void SetB() { }
        }

        [TestMethod]
        [TestCategory("Pointcut selection")]
        public void SetterRulesTest()
        {
            var t = typeof(SetterAdvisedType);
            Assert.IsNull(ArxOne.MrAdvice.Advices.Get(t.GetConstructor(new Type[0])));
            Assert.IsNull(ArxOne.MrAdvice.Advices.Get(t.GetMethod(nameof(SetterAdvisedType.ZetA))));
            Assert.IsNotNull(ArxOne.MrAdvice.Advices.Get(t.GetMethod(nameof(SetterAdvisedType.SetA))));
            Assert.IsNotNull(ArxOne.MrAdvice.Advices.Get(t.GetMethod(nameof(SetterAdvisedType.SetB))));
        }
    }
}
