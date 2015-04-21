using System;

namespace Nontainer
{
    // source and description: http://bartdesmet.net/blogs/bart/archive/2008/03/30/a-functional-c-type-switch.aspx

    /// <summary>
    /// switchFunc gerneric switch
    /// </summary>
    /// <typeparam name="T">the type for the switch</typeparam>
    public class Switch<T>
    {
        /// <summary>
        /// Initializes switchFunc new instance of the <see cref="Switch{T}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public Switch(T @object)
        {
            Object = @object;
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        public T Object { get; private set; }
    }

    /// <summary>
    /// gerneric switch
    /// </summary>
    /// <typeparam name="T">the switch test object</typeparam>
    /// <typeparam name="R"></typeparam>
    public class Switch<T, R>
    {
        public Switch(T o)
        {
            Object = o;
        }

        public T Object { get; private set; }
        public bool HasValue { get; private set; }
        public R Value { get; private set; }

        public void Set(R value)
        {
            Value = value;
            HasValue = true;
        }
    }

    /// <summary>
    /// switchFunc switch object
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Initializes switchFunc new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public Switch(object @object)
        {
            Object = @object;
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        public object Object { get; private set; }
    }

    /// <summary>
    /// none gerneric switch functional
    /// 
    ///       new Switch("test")
    ///            .Case(value => (string) value != "test", () => Console.WriteLine("not a test"))
    ///            .Case(value => (string) value == "test", () => Console.WriteLine("that is a test"));
    /// </summary>
    public static class SwitchExtensions
    {
        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchValue">The switch value.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, object switchValue, Action<object> switchAction)
        {
            return Case(@switch, switchValue, switchAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchValue">The switch value.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, object switchValue, Action switchAction)
        {
            return Case(@switch, switchValue, switchAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchValue">The switch value.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchCondition>true</switchCondition> [fall through].</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, object switchValue, Action<object> switchAction, bool fallThrough)
        {
            return Case(@switch, x => Equals(x, switchValue), switchAction, fallThrough);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchValue">The switch value.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchCondition>true</switchCondition> [fall through].</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, object switchValue, Action switchAction, bool fallThrough)
        {
            return Case(@switch, x => Equals(x, switchValue), switchAction, fallThrough);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, Predicate<object> switchCondition, Action<object> switchAction)
        {
            return Case(@switch, switchCondition, switchAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, Predicate<object> switchCondition, Action switchAction)
        {
            return Case(@switch, switchCondition, switchAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchCondition>true</switchCondition> [fall through].</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, Predicate<object> switchCondition, Action switchAction, bool fallThrough)
        {
            if (@switch == null)
            {
                return null;
            }

            if (switchCondition(@switch.Object))
            {
                switchAction();
                return fallThrough ? @switch : null;
            }

            return @switch;
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchCondition>true</switchCondition> [fall through].</param>
        /// <returns></returns>
        public static Switch Case(this Switch @switch, Predicate<object> switchCondition, Action<object> switchAction, bool fallThrough)
        {
            if (@switch == null)
            {
                return null;
            }

            if (switchCondition(@switch.Object))
            {
                switchAction(@switch.Object);
                return fallThrough ? @switch : null;
            }

            return @switch;
        }

        /// <summary>
        /// switch Default case.
        /// </summary>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">A.</param>
        public static void Default(this Switch @switch, Action<object> switchAction)
        {
            if (@switch != null)
            {
                switchAction(@switch.Object);
            }
        }
    }

    /// <summary>
    /// gerneric switch extension you can specifiy the case input and output(return) type
    /// 
    /// var returnValue = new Switch<string, bool>("test")
    ///            .Case(value => value != "test", stringValue =>
    ///                                                {
    ///                                                    Console.WriteLine("not a test");
    ///                                                    return false;
    ///                                                })
    ///            .Case(value => value == "test", stringValue =>
    ///                                                {
    ///                                                    Console.WriteLine("not a test");
    ///                                                    return true;
    ///                                                });
    ///
    ///         Console.WriteLine(returnValue.Value); 
    /// </summary>
    public static class SwitchGernericExtensionValued
    {
        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">switch type</typeparam>
        /// <typeparam name="R">case return type</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch<T, R> Case<T, R>(this Switch<T, R> @switch, T predicate, Func<T, R> switchAction)
        {
            return Case(@switch, x => Equals(x, predicate), switchAction);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">switch type</typeparam>
        /// <typeparam name="R">case return type</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchPredicate">The switch predicate.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch<T, R> Case<T, R>(this Switch<T, R> @switch, Predicate<T> switchPredicate, Func<T, R> switchAction)
        {
            if (!@switch.HasValue && switchPredicate(@switch.Object))
            {
                @switch.Set(switchAction(@switch.Object));
            }

            return @switch;
        }

        /// <summary>
        /// the switch default
        /// </summary>
        /// <typeparam name="T">switch type</typeparam>
        /// <typeparam name="R">case return type</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static R Default<T, R>(this Switch<T, R> @switch, Func<T, R> switchAction)
        {
            if (!@switch.HasValue)
            {
                @switch.Set(switchAction(@switch.Object));
            }

            return @switch.Value;
        }
    }

    /// <summary>
    /// extension for switchFunc gerneric switch 
    /// 
    ///             new Switch<string>("test")
    ///            .Case(value => value != "test", () => Console.WriteLine("not a test"))
    ///            .Case(value => value == "test", () => Console.WriteLine("that is a test"));
    /// </summary>
    public static class SwitchGernericExtensions
    {
        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Action<T> switchAction) where T : class
        {
            return Case(@switch, o => true, switchAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">A.</param>
        /// <param name="fallThrough">if set to <switchPredicate>true</switchPredicate> [fall through].</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Action<T> switchAction, bool fallThrough) where T : class
        {
            return Case(@switch, o => true, switchAction, fallThrough);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">A.</param>
        /// <param name="fallThrough">if set to <switchPredicate>true</switchPredicate> [fall through].</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Action switchAction, bool fallThrough) where T : class
        {
            return Case(@switch, o => true, switchAction, fallThrough);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="swichAction">The swich action.</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Predicate<T> switchCondition, Action<T> swichAction) where T : class
        {
            return Case(@switch, switchCondition, swichAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="swichAction">The swich action.</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Predicate<T> switchCondition, Action swichAction) where T : class
        {
            return Case(@switch, switchCondition, swichAction, false);
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchPredicate>true</switchPredicate> [fall through].</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Predicate<T> switchCondition, Action<T> switchAction, bool fallThrough) where T : class
        {
            if (@switch == null)
            {
                return null;
            }
            
            if (@switch.Object != null)
            {
                if (switchCondition(@switch.Object))
                {
                    switchAction(@switch.Object);
                    return fallThrough ? @switch : null;
                }
            }

            return @switch;
        }

        /// <summary>
        /// the switch case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchCondition">The switch condition.</param>
        /// <param name="switchAction">The switch action.</param>
        /// <param name="fallThrough">if set to <switchPredicate>true</switchPredicate> [fall through].</param>
        /// <returns></returns>
        public static Switch<T> Case<T>(this Switch<T> @switch, Predicate<T> switchCondition, Action switchAction, bool fallThrough) where T : class
        {
            if (@switch == null)
            {
                return null;
            }

            if (@switch.Object != null)
            {
                if (switchCondition(@switch.Object))
                {
                    switchAction();
                    return fallThrough ? @switch : null;
                }
            }

            return @switch;
        }

        /// <summary>
        /// switch Default case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">The switch action.</param>
        public static void Default<T>(this Switch<T> @switch, Action<T> switchAction)
        {
            if (@switch != null)
            {
                switchAction(@switch.Object);
            }
        }

        /// <summary>
        /// switch Default case.
        /// </summary>
        /// <typeparam name="T">the type of the case</typeparam>
        /// <param name="switch">The switch.</param>
        /// <param name="switchAction">The switch action.</param>
        public static void Default<T>(this Switch<T> @switch, Action switchAction)
        {
            if (@switch != null)
            {
                switchAction();
            }
        }
    }
}
