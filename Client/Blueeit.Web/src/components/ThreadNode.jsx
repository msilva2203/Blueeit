import formatRelativeDate from "../utils/date";
import "./ThreadNode.css";

export default function ThreadNode({thread}) {
    return (
        <>
            <div className="blueeit-threadnode-body">
                <div className="blueeit-threadnode-icon">
                    <div className="blueeit-threadnode-icon-avatar">
                        
                    </div>
                </div>
                <div className="blueeit-threadnode-main">
                    <h3 className="blueeit-threadnode-title">
                        <a href={`threads/${thread.id}`}>
                            {thread.title}
                        </a>
                    </h3>
                    <div className="blueeit-threadnode-minor minor">
                        <p><a href={`/members/${thread.author.id}`}>{thread.author.username}</a></p>
                        <p>{formatRelativeDate(thread.createdAt)}</p>
                    </div>
                </div>
                <div className="blueeit-threadnode-stats">
                    <dl className="blueeit-threadnode-stats-pairs">
                        <dt>
                            Replies:
                        </dt>
                        <dd>
                            20K
                        </dd>
                    </dl>
                    <dl className="blueeit-threadnode-stats-pairs">
                        <dt className="minor">
                            Views:
                        </dt>
                        <dd className="minor">
                            30K
                        </dd>
                    </dl>
                </div>
                <div className="blueeit-threadnode-latest">

                </div>
            </div>
        </>
    );
}