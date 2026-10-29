import ForumNode from "./ForumNode";
import "./ForumNodeList.css";

export default function ForumNodeList({forums}) {
    return (
        <>
            <div className="blueeit-forumnodelist">
                { forums.map((forum, index) => (
                    <div className="blueeit-forumnodelist-item" key={index}>
                        <ForumNode forum={forum} />
                    </div>
                )) }
            </div>
        </>
    );
}