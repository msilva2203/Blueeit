import ProfilePostNode from "./ProfilePostNode";

import "./ProfilePostNodeList.css";

export default function ProfilePostNodeList({posts}) {
    return (
        <>
            <div className="blueeit-profilepostnodelist">
                { posts.map((post, index) => (
                    <div className="blueeit-profilepostnodelist-item" key={index}>
                        <ProfilePostNode post={post} />
                    </div>
                )) }
            </div>
        </>
    );
}