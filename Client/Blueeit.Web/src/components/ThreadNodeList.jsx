import ThreadNode from "./ThreadNode";

import "./ThreadNodeList.css";

export default function ThreadNodeList({threads}) {
    return (
        <>
            <div className="blueeit-threadnodelist">
                { threads.map((thread, index) => (
                    <div className="blueeit-threadnodelist-item" key={index}>
                        <ThreadNode thread={thread} />
                    </div>
                )) }
            </div>
        </>
    );
}