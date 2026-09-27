import "./Post.css";

export default function Post({post}) {
    return (
        <>
            <article className="blueeit-post">
                <div className="blueeit-post-inner">
                    <div className="blueeit-post-user">
                        <div className="blueeit-post-user-avatar">
                            
                        </div>
                        <h3><a href="/members/1">msilva</a></h3>
                    </div>
                    <div className="blueeit-post-main">
                        <header className="blueeit-post-main-header">
                            <p>{post.id}</p>
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