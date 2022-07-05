namespace Axwabo.Helpers.Pools {

    public interface IPoolResettable {

        void Reset(object obj);

        void Destroy(object obj);

    }

}
