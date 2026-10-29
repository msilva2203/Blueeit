import "./Post.css";
import "./ProfilePost.css";

export default function ProfilePost({post}) {
    return (
        <>
            <article className="blueeit-post">
                <div className="blueeit-post-inner">
                    <div className="blueeit-post-user profile">
                        <div className="blueeit-post-user-avatar profile">
                            
                        </div>
                    </div>
                    <div className="blueeit-post-main profile">
                        <header className="blueeit-post-main-header profile">
                            <a href={`/members/${post.author.id}`}><h3>{post.author.username}</h3></a>
                        </header>
                        <div className="blueeit-post-main-content">
                            <p>{post.content}</p>
                        </div>
                    </div>
                </div>
            </article>
        </>
    );
}