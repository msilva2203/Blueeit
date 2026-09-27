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
                        <a href="/members/1">msilva</a>
                        
                    </div>
                </div>
                <div className="blueeit-threadnode-stats">
                    <dl className="blueeit-threadnode-stats-pairs">
                        <dt>
                            Replies:
                        </dt>
                        <dd>
                            0
                        </dd>
                    </dl>
                    <dl className="blueeit-threadnode-stats-pairs">
                        <dt className="minor">
                            Views:
                        </dt>
                        <dd className="minor">
                            0
                        </dd>
                    </dl>
                </div>
                <div className="blueeit-threadnode-latest">

                </div>
            </div>
        </>
    );
}