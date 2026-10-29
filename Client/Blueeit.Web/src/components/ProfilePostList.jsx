import ProfilePost from "./ProfilePost";

import "./ProfilePostList.css";

export default function ProfilePostList({posts}) {
    return (
        <div className="blueeit-profilepostlist">
            { posts.map((post, index) => (
                <div className="blueeit-profilepostlist-item" key={index}>
                    <ProfilePost post={post} />
                </div>
            )) }
        </div>
    );
}