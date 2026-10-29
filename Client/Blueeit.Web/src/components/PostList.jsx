import Post from "./Post";

import "./PostList.css";

export default function PostList({posts}) {
    return (
        <div className="blueeit-postlist">
            { posts.map((post, index) => (
                <div className="blueeit-postlist-item" key={index}>
                    <Post post={post} />
                </div>
            ))}
        </div>
    );
}