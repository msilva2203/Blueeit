import formatRelativeDate from "../../../utils/date";

import "./ActivityNode.css";

export default function ActivityNode({activity}) {
    
    function renderActivityNode() {
        if (activity.type === "ForumCreated") {
            return (
                <>
                    <div>
                        <a href={`/members/${activity.user.id}`}>
                            {activity.user.username}
                        </a>
                        {" created the forum "}
                        <a href={`/forums/${activity.forum.id}`}>
                            {activity.forum.title}
                        </a>
                        {". "}
                    </div>
                    <div>
                        <small>{formatRelativeDate(activity.createdAt)}</small>
                    </div>
                </>
            );
        }

        if (activity.type === "ThreadCreated") {
            return (
                <>
                    <div>
                        <a href={`/members/${activity.user.id}`}>
                            {activity.user.username}
                        </a>
                        {" posted the thread "}
                        <a href={`/forums/threads/${activity.thread.id}`}>
                            {activity.thread.title}
                        </a>
                        {" in "}
                        <a href={`/forums/${activity.forum.id}`}>
                            {activity.forum.title}
                        </a>
                        {". "}
                    </div>
                    <div>
                        <small>{formatRelativeDate(activity.createdAt)}</small>
                    </div>
                </>
            );
        }

        if (activity.type === "PostCreated") {
            return (
                <>
                    <div>
                        <a href={`/members/${activity.user.id}`}>
                            {activity.user.username}
                        </a>
                        {" replied to the thread "}
                        <a href={`/forums/threads/${activity.thread.id}`}>
                            {activity.thread.title}
                        </a>
                        {". "}
                    </div>
                    <div>
                        {activity.post.content}
                    </div>
                    <div>
                        <small>{formatRelativeDate(activity.createdAt)}</small>
                    </div>
                </>
            );
        }

        if (activity.type === "ProfilePostCreated") {
            return (
                <>
                    <div>
                        <a href={`/members/${activity.user.id}`}>
                            {activity.user.username}
                        </a>
                        {" left a message on "}
                        <a href={`/members/${activity.profileOwner.id}`}>
                            {activity.profileOwner.username}
                        </a>
                        {"'s profile."}
                    </div>
                    <div>
                        {activity.profilePost.content}
                    </div>
                    <div>
                        <small>{formatRelativeDate(activity.createdAt)}</small>
                    </div>
                </>
            );
        }

        return (
            <><small>Invalid Activity Type</small></>
        );
    }

    return (
        <>
            <div className="blueeit-activitynode-body">
                <div className="blueeit-activitynode-icon">
                    <div className="blueeit-activitynode-icon-avatar">

                    </div>
                </div>
                <div className="blueeit-activitynode-main">
                    {renderActivityNode()}
                </div>
            </div>
        </>
    );
}