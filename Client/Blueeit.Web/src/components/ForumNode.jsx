import "./ForumNode.css";

export default function ForumNode({forum}) {
    return (
        <>
            <div className="blueeit-forumnode-body">
                <div className="blueeit-forumnode-main">
                    <h3 className="blueeit-forumnode-title">
                        <a href={`/forums/${forum.id}`}>
                            {forum.title}
                        </a>
                    </h3>
                </div>
                <div className="blueeit-forumnode-stats">
                    <dl className="blueeit-forumnode-pairs">
                        <dt className="minor">Threads</dt>
                        <dd>20</dd>
                    </dl>
                    <dl className="blueeit-forumnode-pairs">
                        <dt className="minor">Messages</dt>
                        <dd>999</dd>
                    </dl>
                </div>
            </div>
        </>
    );
}