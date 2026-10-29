import MemberNode from "./MemberNode";

import "./MemberNodeList.css";

export default function MemberNodeList({members}) {
    return (
        <>
            <div className="blueeit-membernodelist">
                { members.map((member, index) => (
                    <div className="blueeit-membernodelist-item" key={index}>
                        <MemberNode member={member} />
                    </div>
                )) }
            </div>
        </>
    );
}