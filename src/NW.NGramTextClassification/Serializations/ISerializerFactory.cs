namespace NW.NGramTextClassification.Serializations
{
    /// <summary>A factory for <see cref="ISerializer{T}"/>.</summary>	
    public interface ISerializerFactory
    {

        /// <summary>Creates a <see cref="ISerializer{T}"/> instance.</summary>	
        ISerializer<T> Create<T>();
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/