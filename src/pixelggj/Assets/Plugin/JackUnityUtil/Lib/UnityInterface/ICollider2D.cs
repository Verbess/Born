using UnityEngine;

namespace JackUtil {

    public interface ICollider2D {

        void OnCollisionEnter2D(Collision2D other);
        void OnCollisionStay2D(Collision2D other);
        void OnCollisionExit2D(Collision2D other);

    }

}