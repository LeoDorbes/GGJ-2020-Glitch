using UnityEngine;

namespace Utils
{
	/// <summary>
	/// If you want to keep a singleton alive between scenes, you must inherit this class
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		private static object _lock = new object();

		protected virtual void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		public static T I
		{
			get
			{
				if (applicationIsQuitting)
				{
					//Debug.LogWarning("[Singleton] Instance " + typeof(T) +
					//" already destroyed on application quit." +
					//"Won't create again - returning null.");
					return null;
				}

				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "(singleton)" + typeof(T);

							DontDestroyOnLoad(singleton);
						}
					}

					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;

		/// <summary>
		/// When unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public virtual void OnApplicationQuit()
		{
			applicationIsQuitting = true;
		}
	}
}