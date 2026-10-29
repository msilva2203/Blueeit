import "./MemberNode.css";

export default function MemberNode({member}) {
    return (
        <>
            <div className="blueeit-membernode-body">
                <div className="blueeit-membernode-icon">
                    <div className="blueeit-membernode-icon-avatar">
                        
                    </div>
                </div>
                <div className="blueeit-membernode-main">
                    <div className="blueeit-membernode-extra">
                        <h1>0</h1>
                    </div>
                    <h3><a href={`/members/${member.id}`}>{member.username}</a></h3>
                </div>
                
            </div>
        </>
    );
}