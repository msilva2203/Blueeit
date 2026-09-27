import Post from "./Post";

import "./PostList.css";

export default function PostList({posts}) {
    return (
        <div className="blueeit-postlist">
            { posts.map((post) => (
                <div className="blueeit-postlist-item">
                    <Post post={post} />
                </div>
            ))}
        </div>
    );
}