using UnityEngine;

namespace JackUtil {

    public interface ITrigger2D {

        void OnTriggerEnter2D(Collider2D other);
        void OnTriggerStay2D(Collider2D other);
        void OnTriggerExit2D(Collider2D other);
        
    }

}