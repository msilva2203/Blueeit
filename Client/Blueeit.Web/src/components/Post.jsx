import formatRelativeDate from "../utils/date";
import "./Post.css";

export default function Post({post}) {
    return (
        <>
            <article className="blueeit-post">
                <div className="blueeit-post-inner">
                    <div className="blueeit-post-user">
                        <div className="blueeit-post-user-avatar">
                            
                        </div>
                        <h3><a href={`/members/${post.author.id}`}>{post.author.username}</a></h3>
                    </div>
                    <div className="blueeit-post-main">
                        <header className="blueeit-post-main-header">
                            <p>{formatRelativeDate(post.createdAt)}</p>
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