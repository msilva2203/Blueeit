import "./ProfilePost.css";

export default function ProfilePostNode({post}) {
    
    
    return (
        <>
            <div className="blueeit-post-inner">
                <div className="blueeit-post-user profile">
                    <div className="blueeit-post-user-avatar profile">
                        
                    </div>
                </div>
                <div className="blueeit-post-main profile">
                    <header className="blueeit-post-main-header profile">
                        <a href={`/members/${post.author.id}`}><h3>{post.author.username}</h3></a>
                        <p>{" -> "}</p>
                        <a href={`/members/${post.user.id}`}><h3>{post.user.username}</h3></a>
                    </header>
                    <div className="blueeit-post-main-content">
                        <p>{post.content}</p>
                    </div>
                </div>
            </div>
        </>
    );
}