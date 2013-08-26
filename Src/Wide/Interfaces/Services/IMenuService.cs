namespace Wide.Interfaces.Services
{
    public interface IMenuService
    {
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        /// <exception cref="System.ArgumentException">Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.</exception>
        string Add(AbstractCommandable item);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="GuidString">The unique GUID set for the menu available for the creator.</param>
        /// <returns><c>true</c> if successfully removed, <c>false</c> otherwise</returns>
        bool Remove(string GuidString);

        /// <summary>
        /// Gets the node with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>`0.</returns>
        AbstractCommandable Get(string key);

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        void Refresh();
    }
}