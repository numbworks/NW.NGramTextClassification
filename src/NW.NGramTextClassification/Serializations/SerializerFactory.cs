namespace NW.NGramTextClassification.Serializations
{
    /// <inheritdoc cref="ISerializerFactory"/>
    public class SerializerFactory : ISerializerFactory
    {

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SerializerFactory"/> instance using default parameters.</summary>	
        public SerializerFactory() { }

        #endregion

        #region Methods_public

        public ISerializer<T> Create<T>()
        {

            return new Serializer<T>();

        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/